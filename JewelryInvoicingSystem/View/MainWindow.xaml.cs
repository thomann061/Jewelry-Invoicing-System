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
                btnExit.IsEnabled = false;

                //Default Constructors
                mainViewModel.Invoice = new Invoice();
                mainViewModel.InvoiceItems.Clear();

                //get next id for an invoice
                mainViewModel.Invoice.InvoiceDate = DateTime.Now;
                mainViewModel.Invoice.InvoiceCode = ja.selectNextInvoiceId();
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
                if (selectedItem != null) {
                    InvoiceItem newInvoiceItem = new InvoiceItem();
                    newInvoiceItem.Item = selectedItem;
                    //set the InvoiceItem to an observable array
                    mainViewModel.InvoiceItems.Add(newInvoiceItem);
                    //store in database
                    if (btnEditInvoice.Content.ToString() == "Done Editing") {
                        ja.insertInvoiceItem(newInvoiceItem, mainViewModel.Invoice);

                    }
                }
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
                    mainViewModel.InvoiceItems = ja.selectItemsFromInvoice(srchWin.ReturnInvoice.InvoiceCode);
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
                if (dtePck.SelectedDate == null || dataGrid.Items.Count == 0)
                    MessageBox.Show("You must enter an invoice date or at least one item.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else {
                    //insert invoice
                    if (ja.insertInvoice(mainViewModel.Invoice)) {
                        bool result = ja.updateInvoiceWithItems(mainViewModel.Invoice, mainViewModel.InvoiceItems);
                        if (result) {
                            //enable data fields for use
                            btnAddItem.IsEnabled = false;
                            btnDeleteitem.IsEnabled = false;
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
                btnDeleteitem.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnSearchInvoice.IsEnabled = true;
                btnInventory.IsEnabled = true;

                //clear defaults
                mainViewModel.Invoice = null;
                mainViewModel.InvoiceItems.Clear();
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

        /// <summary>
        /// Handler for editing an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e) {
            if (btnEditInvoice.Content.ToString() == "Edit Invoice") {
                //enable data fields for use
                btnAddItem.IsEnabled = true;
                btnDeleteInvoice.IsEnabled = false;
                btnDeleteitem.IsEnabled = true;
                btnNewInvoice.IsEnabled = false;
                dtePck.IsEnabled = true;
                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;
                txtTotalCostCount.IsEnabled = true;
                btnSearchInvoice.IsEnabled = false;
                btnInventory.IsEnabled = false;
                dataGrid.IsEnabled = true;
                btnEditInvoice.IsEnabled = true;
                btnEditInvoice.Content = "Done Editing";
            } else {
                btnAddItem.IsEnabled = false;
                btnDeleteitem.IsEnabled = false;
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
                btnEditInvoice.Content = "Edit Invoice";
            }

        }

        /// <summary>
        /// Delete an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Delete an invoice item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItm_Click(object sender, RoutedEventArgs e) {
            InvoiceItem selectedItem = (InvoiceItem)dataGrid.SelectedItem;
            if (selectedItem != null)
                if (btnEditInvoice.Content.ToString() == "Done Editing")
                    ja.deleteInvoiceItem(selectedItem);
                mainViewModel.InvoiceItems.Remove(selectedItem);
        }
    }//end Main Window
}
