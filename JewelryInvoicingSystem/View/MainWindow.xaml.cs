using JewelryInvoicingSystem.Model;
using JewelryInvoicingSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JewelryInvoicingSystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private JewelryAccess ja;
        
        public MainWindow()
        {
            mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;
            ja = new JewelryAccess();
            mainViewModel.Items = ja.selectItems(); //populate items box
            InitializeComponent();
        }

        

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// On click of "New Invoice", this method enables the data fields for user input. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mainViewModel.Invoice != null) {
                    mainViewModel.Invoice = null;
                    mainViewModel.InvoiceItems.Clear();
                }
                //enable data fields for use
                btnAddItem.IsEnabled = true;
                btnDeleteitem.IsEnabled = true;
                btnNewInvoice.IsEnabled = false;
                dtePck.IsEnabled = true;
                btnCancel.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnDeleteInvoice.IsEnabled = false;
                btnEditInvoice.IsEnabled = false;
                txtTotalCostCount.IsEnabled = true;
                btnSearchInvoice.IsEnabled = false;
                btnInventory.IsEnabled = false;
                dataGrid.IsEnabled = true;

                //create a new invoice
                Invoice newInvoice = new Invoice();
                //insert invoice into database
                int id = ja.insertInvoice(newInvoice);
                if (id != 0) {
                    //update invoice with id
                    newInvoice.InvoiceCode = id;
                    newInvoice.InvoiceDate = DateTime.Now;
                    //set new invoice to the view
                    mainViewModel.Invoice = newInvoice;
                    //unset ReadOnly from datagrid
                    dataGrid.IsReadOnly = false;
                }
            }

            catch
            {
               MessageBox.Show("Sorry, something went wrong!", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This method will add the item the user entered into the database. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create a new InvoiceItem from Item and Cost
                Item selectedItem = (Item) cbxItem.SelectedItem;
                double cost = int.Parse(txtTotalCostCount.Text.ToString());
                InvoiceItem newInvoiceItem = new InvoiceItem();
                newInvoiceItem.Item = selectedItem;
                newInvoiceItem.Item.ItemCost = cost;
                //set the InvoiceItem to an observable array
                mainViewModel.InvoiceItems.Add(newInvoiceItem);
            }
            catch
            {
                System.Windows.MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Opens up the search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchWindow srchWin = new SearchWindow(this);
                srchWin.ShowDialog();
                //if the invoice is not null, set it to the current invoice
                if(srchWin.ReturnInvoice != null) {
                    mainViewModel.Invoice = srchWin.ReturnInvoice;
                    //data bind the InvoiceItems
                    dataGrid.IsReadOnly = false;
                    mainViewModel.InvoiceItems.Clear();
                    mainViewModel.InvoiceItems = ja.selectItemsFromInvoice(srchWin.ReturnInvoice.InvoiceCode);
                    //ObservableCollection<InvoiceItem> items = ja.selectItemsFromInvoice(srchWin.ReturnInvoice.InvoiceCode);
                    //set to ReadOnly
                    dataGrid.IsReadOnly = true;
                    //enable edit and delete
                    btnDeleteInvoice.IsEnabled = true;
                    btnEditInvoice.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Opens up the Inventory Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DefWindow defWin = new DefWindow(this);
                defWin.ShowDialog();
                
                
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Saves the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNewInvoice.IsEnabled = true;
                //set the date for the invoice
                if (dtePck.SelectedDate == null)
                    MessageBox.Show("You must enter an invoice date!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else {
                    bool result = ja.updateInvoiceWithItems(mainViewModel.Invoice, mainViewModel.InvoiceItems);
                    if (result) {
                        //enable data fields for use
                        btnAddItem.IsEnabled = false;
                        btnNewInvoice.IsEnabled = true;
                        dtePck.IsEnabled = false;
                        btnCancel.IsEnabled = false;
                        btnDeleteInvoice.IsEnabled = true;
                        btnEditInvoice.IsEnabled = true;
                        btnSave.IsEnabled = false;
                        txtTotalCostCount.IsEnabled = false;
                        btnSearchInvoice.IsEnabled = true;
                        btnInventory.IsEnabled = true;
                        dataGrid.IsEnabled = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Erases the data the user input on the form and puts the window back to it's initial state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //enable data fields for use
                btnAddItem.IsEnabled = false;
                btnNewInvoice.IsEnabled = true;
                dtePck.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;
                dtePck.Text = "";
                btnSearchInvoice.IsEnabled = true;
                btnInventory.IsEnabled = true;
            }

            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                     MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Closes the window and exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
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

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e) {
            //enable data fields for use
            btnAddItem.IsEnabled = true;
            btnDeleteitem.IsEnabled = true;
            btnNewInvoice.IsEnabled = false;
            dtePck.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;
            txtTotalCostCount.IsEnabled = true;
            btnSearchInvoice.IsEnabled = true;
            btnInventory.IsEnabled = true;
            dataGrid.IsEnabled = true;
            btnEditInvoice.IsEnabled = true;
        }

        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e) {
            //delete an invoice
            bool result = ja.deleteInvoice(mainViewModel.Invoice);
            if(result) {
                mainViewModel.Invoice = null;
                mainViewModel.InvoiceItems.Clear();
                //enable data fields for use
                btnEditInvoice.IsEnabled = false;
                btnDeleteInvoice.IsEnabled = false;
            }
        }

        private void btnDeleteItm_Click(object sender, RoutedEventArgs e) {
            InvoiceItem selectedItem = (InvoiceItem)dataGrid.SelectedItem;
            if(ja.deleteItem(selectedItem.Item.ItemCode))
                mainViewModel.InvoiceItems.Remove(selectedItem);
        }
    }//end Main Window
}
