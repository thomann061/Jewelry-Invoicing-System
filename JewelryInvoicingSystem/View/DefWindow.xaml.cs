﻿using JewelryInvoicingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Item> _items;
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<InvoiceItem> _invoiceItems;
        private JewelryAccess ja;
        private Item _returnItems;
        DataAccess db = new DataAccess();
        DataSet ds = new DataSet();

        public Item ReturnItems
        {
            get { return _returnItems; }
            set { _returnItems = value; }
        }

        public DefWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ja = new JewelryAccess();
            Items = new ObservableCollection<Item>();
            //Invoices = new ObservableCollection<Invoice>();
            //InvoiceItems = new ObservableCollection<InvoiceItem>();


            //select all invoices
            //ObservableCollection<Item> initialItems = ja.selectItems();

            Items = ja.selectItemNameAndDesc();
            dtaGrdInventory.ItemsSource = Items;
            //dtaGrdInventory.IsReadOnly = true;

        }

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public ObservableCollection<Invoice> Invoices
        {
            get { return _invoices; }
            set { _invoices = value; }
        }

        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get { return _invoiceItems; }
            set { _invoiceItems = value; }
        }

        private void populateItemsComboBox()
        {
            Items = ja.selectItems();
            //cbxItem.ItemsSource = Items;
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

                if (txtCost.Text != "" || txtItemDescription.Text != "" || txtName.Text != "")
                {

                    //Create a new item
                    Item newItem = new Item();

                    //Extract the text from the text fields and set them equal to a new Item.
                    newItem.ItemName = txtName.Text.ToString();
                    newItem.ItemDesc = txtItemDescription.Text.ToString();
                    newItem.ItemCost = int.Parse(txtCost.Text.ToString());


                    //set the InvoiceItem to an observable array
                    //Items.Add(newItem);
                    //data bind it
                    //dtaGrdInventory.ItemsSource = Items;

                    //insert the item into the database
                    ja.insertItem(newItem);


                    //enable fields
                    txtName.Text = txtCost.Text = txtItemDescription.Text = "";
                    btnSaveAndClose.IsEnabled = true;

                    ReturnItems = newItem; 
                }
                

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
                //disable the new and delete item button so ensure editing of current item
                btnAddNew.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnCancel.Visibility = Visibility.Visible;

               // dtaGrdInventory.CurrentCell

                ////Make sure the current row is not null
                //if (dtaGrdInventory.CurrentCell != null)
                //{

                //    //Check to see if the user is deleting a row
                //    if (IsDeleting == false)
                //    {
                //        //Gives the current cell's row number
                //        int iRowNum = dataGridView1.CurrentCell.RowIndex;
                //        //or
                //        iRowNum = dataGridView1.CurrentRow.Index;

                //        //Make sure a valid row is selected
                //        if (iRowNum < ds.Tables[0].Rows.Count)
                //        {
                //            //Put the highlighted row's data in the textboxes
                //            txtUpdateFirstName.Text = ds.Tables[0].Rows[iRowNum][1].ToString();
                //            txtUpdateLastName.Text = ds.Tables[0].Rows[iRowNum].ItemArray[2].ToString();
                //        }
                //    }
                //}

            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        private void dtaGrdInventory_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                //Make sure the current row is not null
                if (dtaGrdInventory.SelectedItem != null)
                {

                    //Check to see if the user is deleting a row
                    //if (IsDeleting == false)
                    //{
                    //Gives the current cell's row number
                    int iRowNum = int.Parse(dtaGrdInventory.SelectedCells.ToString());
                        //or
                        //iRowNum = dtaGrdInventory.CurrentRow.Index;

                        //Make sure a valid row is selected
                        if(iRowNum < ds.Tables[0].Rows.Count)
                        {
                            //Put the highlighted row's data in the textboxes
                            txtName.Text = ds.Tables[0].Rows[iRowNum][1].ToString();
                            txtCost.Text = ds.Tables[0].Rows[iRowNum].ItemArray[2].ToString();
                            txtItemDescription.Text = ds.Tables[0].Rows[iRowNum].ItemArray[3].ToString();
                        }
                    //}
                }
            }
            catch (Exception)
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


                if (txtCost.Text != "" || txtItemDescription.Text != "" || txtName.Text != "")
                {

                    //Create a new item
                    Item newItem = new Item();


                    //Extract the text from the text fields and set them equal to a new Item.
                    newItem.ItemName = txtName.Text.ToString();
                    newItem.ItemDesc = txtItemDescription.Text.ToString();
                    newItem.ItemCost = int.Parse(txtCost.Text.ToString());


                    //set the InvoiceItem to an observable array
                    Items.Add(newItem);
                    //data bind it
                    dtaGrdInventory.ItemsSource = Items;

                    //enable fields
                    txtName.Text = txtCost.Text = txtItemDescription.Text = "";
                    btnSaveAndClose.IsEnabled = true;

                    ReturnItems = newItem;
                }
                else
                {
                    //if there is no data entered
                    MessageBox.Show("Please enter data into the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch 
            {
                MessageBox.Show("Sorry, something went wrong! Please enter appropriate data.", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtaGrdInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender != null)
            { }
        }
    }//Def Window
}
