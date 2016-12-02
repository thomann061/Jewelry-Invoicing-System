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
        private ObservableCollection<Item> _items;
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<InvoiceItem> _invoiceItems;
        private JewelryAccess ja;
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public ObservableCollection<Item> Items {
            get { return _items; }
            set { _items = value; }
        }

        public ObservableCollection<Invoice> Invoices {
            get { return _invoices; }
            set {
                if (value != _invoices) {
                    _invoices = value;
                    OnPropertyChanged("Invoices");
                }
            }
        }

        public ObservableCollection<InvoiceItem> InvoiceItems {
            get { return _invoiceItems; }
            set {
                if (value != _invoiceItems) {
                    _invoiceItems = value;
                    OnPropertyChanged("InvoiceItems");
                }
            }
        }

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
            bool result = ja.deleteInvoice(Invoices);
            if(result) {
                Invoices.Clear();
                InvoiceItems.Clear();
                //enable data fields for use
                btnEditInvoice.IsEnabled = false;
                btnDeleteInvoice.IsEnabled = false;
            }
        }

       
        private void txtTotalCostCount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void itemSelected(object sender, SelectionChangedEventArgs e) {
            Item selectedItem = (Item)cbxItem.SelectedItem;
            txtTotalCostCount.Text = selectedItem.ItemCost.ToString();
        }

        private void btnDeleteItm_Click(object sender, RoutedEventArgs e)
        {
            InvoiceItem selectedItem = (InvoiceItem)dataGrid.SelectedItem;
            
            InvoiceItems.Remove(selectedItem);
        }

    }//end Main Window
}
