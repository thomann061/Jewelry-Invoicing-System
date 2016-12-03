using JewelryInvoicingSystem.Model;
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
        private MainWindow mainWindow;
        private DefViewModel defViewModel;
        private JewelryAccess ja;
        private Item selectedItem;
        private int selectedIndex;

        public DefWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            defViewModel = new DefViewModel();
            this.DataContext = defViewModel;
            this.mainWindow = mainWindow;
            ja = new JewelryAccess();

            //select all invoices
            defViewModel.Items = new ObservableCollection<Item>();
            //set to the view
            defViewModel.Items = ja.selectItems();
        }


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
                //enable/disable fields for use
                txtCost.IsEnabled = true;
                txtName.IsEnabled = true;
                txtItemDescription.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnClose.IsEnabled = true;
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
                //Closes the form
                Close();

            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


///////the close and cancel button could probably be put into one button. My idea was the user could click cancel and leave the
///////window without saving anything. Alternatively, clicking on the close button would close the window while saving the  
///////information


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Closes the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
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
                ///TODO: POPULATE THE NAME, COST, AND DESCRIPTION FIELDS WITH THE ITEM THE USER HAS SELECTED
                ///TO BE EDITED IN THE DATAGRID
                

                //get first selected item
                selectedItem = (Item) dtaGrdInventory.SelectedItems[0];
                selectedIndex = dtaGrdInventory.SelectedIndex;
                //bring text to screen
                if (selectedItem != null) {
                    //disable the new and delete item button so ensure editing of current item
                    btnAddNew.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    txtName.IsEnabled = true;
                    txtItemDescription.IsEnabled = true;
                    txtCost.IsEnabled = true;
                    btnSave.IsEnabled = true;
                    btnCancel.Visibility = Visibility.Visible;
                    //set text to screen
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
                if (selectedItem != null) {
                    selectedItem.ItemName = txtName.Text.ToString();
                    selectedItem.ItemDesc = txtItemDescription.Text.ToString();
                    selectedItem.ItemCost = int.Parse(txtCost.Text.ToString());
                    for (int i = 0; i < defViewModel.Items.Count; i++) {
                        if (defViewModel.Items[i].ItemCode == selectedItem.ItemCode)
                            defViewModel.Items[i] = selectedItem;
                    }
                    ja.updateItem(selectedItem); //update in database
                    selectedItem = null;
                } else if (txtCost.Text != "" || txtItemDescription.Text != "" || txtName.Text != "")
                {

                    //Create a new item
                    Item newItem = new Item();


                    //Extract the text from the text fields and set them equal to a new Item.
                    newItem.ItemName = txtName.Text.ToString();
                    newItem.ItemDesc = txtItemDescription.Text.ToString();
                    newItem.ItemCost = int.Parse(txtCost.Text.ToString());

                    bool result = ja.insertItem(newItem);
                    if(result)
                        defViewModel.Items.Add(newItem);
                    //set the InvoiceItem to an observable array

                    //data bind it
                    //dtaGrdInventory.ItemsSource = Items;

                }
                else
                {
                    //if there is no data entered
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get first selected item
                selectedItem = (Item)dtaGrdInventory.SelectedItems[0];
                selectedIndex = dtaGrdInventory.SelectedIndex;

                int itemId = selectedItem.ItemCode;

                bool result = ja.deleteItem(itemId);
                if (result)
                {
                    defViewModel.Items.RemoveAt(selectedIndex);
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
      
    }//Def Window
}
