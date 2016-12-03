using JewelryInvoicingSystem.Model;
using JewelryInvoicingSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window {
        private MainWindow mainWindow;
        private JewelryAccess ja;
        private SearchViewModel searchViewModel;
        private Invoice _returnInvoice;

        public Invoice ReturnInvoice {
            get { return _returnInvoice; }
            set { _returnInvoice = value; }
        }

        public SearchWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            searchViewModel = new SearchViewModel();
            this.DataContext = searchViewModel;
            this.mainWindow = mainWindow;
            //load in all invoices to the combobox
            ja = new JewelryAccess();
            //select all invoices
            ObservableCollection<Invoice> invoices = ja.selectInvoices();
            searchViewModel.Invoices = invoices;
            searchViewModel.ComboBoxInvoices = invoices;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Close window on Cancel button click. Does not save anything on the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// When the user selects an invoice this method passes the data back to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO: PASS THE DATA BACK TO THE MAIN FORM. WHEN WE PASS THE DATA BACK, WE MUST ALSO DISABLE BUTTONS.
                //ALL BUTTONS SHOULD BE DISABLED EXCEPT FOR CANCEL, EDIT INVOICE, AND DELETE INVOICE.
                //get selected invoice
                ReturnInvoice = (Invoice)dtaGrdsInvoices.SelectedItem; //set the as the invoice to be returned
                this.Close();
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cbxSelectionChanged(object sender, SelectionChangedEventArgs e) {
            //get the selected invoice
            Invoice invoice = (Invoice)cbxCriteria1.SelectedItem;
            //query the database with the invoice id and put the results into an observable array
            searchViewModel.Invoices = ja.selectInvoiceById(invoice.InvoiceCode);
        }

        private void dateWasChanged(object sender, SelectionChangedEventArgs e) {
            //get the selected date
            string date = datePicker.Text;
            //query the database with the date and put results into an observable array
            searchViewModel.Invoices = ja.selectInvoicesByDate(date);
        }

        private void totalCostChanged(object sender, SelectionChangedEventArgs e) {
            //get the selected invoice
            Invoice invoice = (Invoice)cbxCriteria3.SelectedItem;
            //query the database with the invoice id and put the results into an observable array
            searchViewModel.Invoices = ja.selectInvoicesByTotal(invoice.InvoiceTotal);
        }
    }//end Search Window
}
