﻿using JewelryInvoicingSystem.Model;
using JewelryInvoicingSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JewelryInvoicingSystem {
    /// <summary>
    /// Interaction logic for DefWindow.xaml
    /// </summary>
    public partial class DefWindow : Window {
        /// <summary>
        /// Main Window instance
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// defViewModel instance
        /// </summary>
        private DefViewModel defViewModel;

        /// <summary>
        /// Jewelry Access instance
        /// </summary>
        private JewelryAccess ja;

        /// <summary>
        /// Holds selected item
        /// </summary>
        private Item selectedItem;

        /// <summary>
        /// Holds the selected index
        /// </summary>
        private int selectedIndex;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DefWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            defViewModel = new DefViewModel();
            this.DataContext = defViewModel;
            this.mainWindow = mainWindow;
            ja = new JewelryAccess();

            //set to the view
            defViewModel.Items = ja.selectItems();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Methods

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Enables fields for user entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtaGrdInventory.SelectedItems.Clear();

                //enable/disable fields for use
                btnAddNew.IsEnabled = false;
                txtCost.IsEnabled = true;
                txtName.IsEnabled = true;
                txtItemDescription.IsEnabled = true;
                btnSave.IsEnabled = true;
                txtCost.Background = Brushes.BlanchedAlmond;
                txtItemDescription.Background = Brushes.BlanchedAlmond;
                txtName.Background = Brushes.BlanchedAlmond;
                btnCancel.Visibility = Visibility.Visible;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Saves the new item that was entered and closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Puts the window back into its initial state without saving. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //disable the buttons
                txtCost.IsEnabled = 
                txtName.IsEnabled = 
                txtItemDescription.IsEnabled = false;

                //enable action buttons
                btnSave.IsEnabled = false;
                btnAddNew.IsEnabled = 
                btnDelete.IsEnabled = 
                btnEdit.IsEnabled = true;

                //clear the fields
                txtName.Text = 
                txtCost.Text = 
                txtItemDescription.Text = "";

                //Hide the cancel button
                btnCancel.Visibility = Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// When a user has chosen an item in the data grid to edit, this will populate the fields with the data for the user to edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get first selected item
                selectedItem = (Item) dtaGrdInventory.SelectedItem;
                selectedIndex = dtaGrdInventory.SelectedIndex;

                //bring text to screen
                if (selectedItem != null)
                {
                    //Disable the new and delete item button so ensure editing of current item
                    btnAddNew.IsEnabled = false;
                    btnEdit.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    txtName.IsEnabled = true;
                    txtItemDescription.IsEnabled = true;
                    txtCost.IsEnabled = true;
                    btnSave.IsEnabled = true;
                    btnCancel.Visibility = Visibility.Visible;

                    //Set text to screen
                    txtName.Text = selectedItem.ItemName;
                    txtItemDescription.Text = selectedItem.ItemDesc;
                    txtCost.Text = selectedItem.ItemCost.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Saves the item to the database and clears 'Add new item' data fields for the user for re-entry. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double outs;
                if (selectedItem != null)
                {
                    selectedItem.ItemName = txtName.Text.ToString();
                    selectedItem.ItemDesc = txtItemDescription.Text.ToString();
                    selectedItem.ItemCost = int.Parse(txtCost.Text.ToString());
                    for (int i = 0; i < defViewModel.Items.Count; i++) {
                        if (defViewModel.Items[i].ItemCode == selectedItem.ItemCode)
                            defViewModel.Items[i] = selectedItem;
                    }
                    ja.updateItem(selectedItem); //update in database
                    selectedItem = null;
                } else if (txtCost.Text != "" || double.TryParse(txtCost.Text, out outs) || txtItemDescription.Text != "" || txtName.Text != "")
                {
                    //Create a new item
                    Item newItem = new Item();

                    //Extract the text from the text fields and set them equal to a new Item.
                    newItem.ItemName = txtName.Text.ToString();
                    newItem.ItemDesc = txtItemDescription.Text.ToString();
                    newItem.ItemCost = double.Parse(txtCost.Text.ToString());

                    bool result = ja.insertItem(newItem);
                    if (result)
                        defViewModel.Items.Add(newItem);
                }
                else
                {
                    //If there is no data entered
                    MessageBox.Show("Please enter data into the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }

                //enable fields
                txtName.Text = txtCost.Text = txtItemDescription.Text = "";
                btnSaveAndClose.IsEnabled = true;
                btnAddNew.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnEdit.IsEnabled = true;
            }
            catch 
            {
                MessageBox.Show("Sorry, something went wrong! Please enter appropriate data.", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Deletes an item from the Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get first selected item
                selectedItem = (Item)dtaGrdInventory.SelectedItem;
                selectedIndex = dtaGrdInventory.SelectedIndex;
                if(selectedItem != null) {
                    int itemId = selectedItem.ItemCode;

                    bool result = ja.deleteItem(itemId);
                    if (result) {
                        defViewModel.Items.RemoveAt(selectedIndex);
                    } else
                        MessageBox.Show("Item is in use! Shame, Shame, Shame!");
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

    }//Def Window
}
