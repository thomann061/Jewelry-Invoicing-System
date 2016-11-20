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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JewelryInvoicingSystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
                btnDeleteItem.IsEnabled = true;
                btnAddItem.IsEnabled = true;
                dtePck.IsEnabled = true;
                btnCancel.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnSearchInvoice.IsEnabled = false;
                btnInventory.IsEnabled = false;
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
                SearchWindow srchWin = new SearchWindow();
                srchWin.ShowDialog();
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
                DefWindow defWin = new DefWindow();
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
                btnDeleteItem.IsEnabled = false;
                btnAddItem.IsEnabled = false;
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
    }//end Main Window
}
