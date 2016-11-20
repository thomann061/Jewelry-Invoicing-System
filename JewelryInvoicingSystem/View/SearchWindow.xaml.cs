using System;
using System.Collections.Generic;
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
        public SearchWindow()
        {
            InitializeComponent();

            //No data will be passed into the window here since the user only wishes to search invoices for now. 
            //Later, we wil pass what data the user wants back to the main window 
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
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }//end Search Window
}
