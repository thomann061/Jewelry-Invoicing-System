using JewelryInvoicingSystem.Model;
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

        /// <summary>
        /// Item Observable Collection
        /// </summary>
        private ObservableCollection<Item> _items;

        /// <summary>
        /// Invoice Observable Collection
        /// </summary>
        private ObservableCollection<Invoice> _invoices;

        /// <summary>
        /// Invoice Items Observable Collection
        /// </summary>
        private ObservableCollection<InvoiceItem> _invoiceItems;

        /// <summary>
        /// JewelryAccess instance
        /// </summary>
        private JewelryAccess ja;

        /// <summary>
        /// Keeps track of when the user is trying to delete
        /// </summary>
        private bool isDeleting = false;
       
        /// <summary>
        /// Propert changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        /// <summary>
        /// On Property Changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Items Property
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        /// <summary>
        /// Invoices property
        /// </summary>
        public ObservableCollection<Invoice> Invoices
        {
            get { return _invoices; }
            set
            {
                if (value != _invoices)
                {
                    _invoices = value;
                    OnPropertyChanged("Invoices");
                }
            }
        }
        /// <summary>
        /// Invoice Items Property
        /// </summary>
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get { return _invoiceItems; }
            set
            {
                if (value != _invoiceItems)
                {
                    _invoiceItems = value;
                    OnPropertyChanged("InvoiceItems");
                }
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MainWindow()
        {
            this.DataContext = this;
            ja = new JewelryAccess();
            Items = new ObservableCollection<Item>();
            Invoices = new ObservableCollection<Invoice>();
            InvoiceItems = new ObservableCollection<InvoiceItem>();
            InitializeComponent();
            populateItemsComboBox();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Methods

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Populates the combo boxes
        /// </summary>
        private void populateItemsComboBox() {
            Items = ja.selectItems();
            cbxItem.ItemsSource = Items;
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
                if (Invoices.Count != 0) {
                    Invoices.Clear();
                    InvoiceItems.Clear();
                }
                //enable data fields for use
                btnAddItem.IsEnabled = true;
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
                //update invoice with id
                newInvoice.InvoiceCode = id;
                //add to observable array
                Invoices.Add(newInvoice);
                //data bind the label and date
                Binding b = new Binding("InvoiceCode");
                b.Mode = BindingMode.TwoWay;
                b.Source = newInvoice;
                lblInvoice.SetBinding(Label.ContentProperty, b);
                //unset ReadOnly from datagrid
                dataGrid.IsReadOnly = false;
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
                //for each item in the data grid, lets extract the cost out and add it all together

                //create a new InvoiceItem from Item and Cost
                Item selectedItem = (Item) cbxItem.SelectedItem;
                double cost = int.Parse(txtTotalCostCount.Text.ToString());
                InvoiceItem newInvoiceItem = new InvoiceItem();
                newInvoiceItem.Item = selectedItem;
                newInvoiceItem.ItemCost = cost;
                //set the InvoiceItem to an observable array
                InvoiceItems.Add(newInvoiceItem);
                //data bind it
                //dataGrid.ItemsSource = InvoiceItems;
                btnDeleteitem.IsEnabled = true;

                totalRunningCost();

            }
            catch
            {
                System.Windows.MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Keeps track of the total cost of the entire invoice
        /// </summary>
        private void totalRunningCost()
        {
            try
            {
                int cost = 0;
                int totalCost = 0;

                //check for values in the data grid
                if (dataGrid == null)
                {
                    lblRunningTotal.Content = "";
                }
                else if (!isDeleting) //if we aren't deleting
                {
                    cost = 0;
                    totalCost = 0;
                    //run through invoice items and add them together
                    for (int i = 0; i < InvoiceItems.Count; i++)
                    {
                        cost = int.Parse(InvoiceItems[i].ItemCost.ToString());
                        totalCost += cost;
                        lblRunningTotal.Content = totalCost;
                    }
                }
               else if(isDeleting) //and if we are deleting
                {
                    //total cost from the label
                    totalCost = int.Parse(lblRunningTotal.Content.ToString());

                    //grab our selection
                    InvoiceItem selectedItem = (InvoiceItem)dataGrid.SelectedItem;

                    //grab the cost of the selected item and subtract it from the total cost
                    cost = int.Parse(selectedItem.ItemCost.ToString());
                    totalCost -= cost;

                    //Display results
                    lblRunningTotal.Content = totalCost;

                    isDeleting = false;
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
                    Invoices.Clear();
                    Invoices.Add(srchWin.ReturnInvoice);
                    //data bind the InvoiceCode
                    Binding b = new Binding("InvoiceCode");
                    b.Mode = BindingMode.TwoWay;
                    b.Source = srchWin.ReturnInvoice;
                    lblInvoice.SetBinding(Label.ContentProperty, b);
                    //data bind the InvoiceDate
                    Binding b2 = new Binding("InvoiceDate");
                    b2.Mode = BindingMode.TwoWay;
                    b2.Source = srchWin.ReturnInvoice;
                    dtePck.SetBinding(DatePicker.TextProperty, b2);
                    //data bind the InvoiceItems
                    InvoiceItems.Clear();
                    dataGrid.ItemsSource = ja.selectItemsFromInvoice(srchWin.ReturnInvoice.InvoiceCode);
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
                populateItemsComboBox();
                
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
                    Invoice newInvoice = Invoices.ElementAt(0);
                    newInvoice.InvoiceDate = (DateTime)dtePck.SelectedDate;

                    //data bind the InvoiceDate
                    Binding b2 = new Binding("InvoiceDate");
                    b2.Mode = BindingMode.TwoWay;
                    b2.Source = newInvoice;
                    dtePck.SetBinding(DatePicker.TextProperty, b2);
                    bool result = ja.updateInvoiceWithItems(Invoices, InvoiceItems);
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
                        cbxItem.IsEnabled = false;
                        btnDeleteitem.IsEnabled = false;
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
                btnDeleteitem.IsEnabled = false;
                Invoices.Clear();
                InvoiceItems.Clear();
                lblRunningTotal.Content = "";
                txtTotalCostCount.Text = "";
                lblInvoice.Content = "";
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Allows the user to edit an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e) {

            try
            {
                //enable data fields for use
                btnAddItem.IsEnabled = true;
                btnNewInvoice.IsEnabled = false;
                dtePck.IsEnabled = true;
                btnCancel.IsEnabled = true;
                btnSave.IsEnabled = true;
                txtTotalCostCount.IsEnabled = true;
                btnSearchInvoice.IsEnabled = true;
                btnInventory.IsEnabled = true;
                dataGrid.IsEnabled = true;
                btnEditInvoice.IsEnabled = true;
                btnDeleteitem.IsEnabled = true;
                cbxItem.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Deletes the current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e) {

            try
            {
                //delete an invoice
                bool result = ja.deleteInvoice(Invoices);
                if (result)
                {
                    Invoices.Clear();
                    InvoiceItems.Clear();
                    //enable data fields for use
                    btnEditInvoice.IsEnabled = false;
                    btnDeleteInvoice.IsEnabled = false;
                    lblInvoice.Content = "";
                    dtePck.Text = "";
                    txtTotalCostCount.Text = "";
                    lblRunningTotal.Content = "";
                }
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
         }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Don't think this is really used but everything crashes whenever I try to delete it. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTotalCostCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //nothing
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Selects an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelected(object sender, SelectionChangedEventArgs e) {

            try
            {
                Item selectedItem = (Item)cbxItem.SelectedItem;
                txtTotalCostCount.Text = selectedItem.ItemCost.ToString();
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Deletes an item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceItem selectedItem = (InvoiceItem)dataGrid.SelectedItem;
                isDeleting = true;
                totalRunningCost();
                InvoiceItems.Remove(selectedItem);
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

    }//end Main Window
}
