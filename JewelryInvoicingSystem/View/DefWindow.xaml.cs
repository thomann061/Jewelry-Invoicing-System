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
    /// Interaction logic for DefWindow.xaml
    /// </summary>
    public partial class DefWindow : Window {
        public DefWindow()
        {
            InitializeComponent();
            //No data will be passed INTO the window here since the user only wishes to update inventory for now. 
            //Later, we wil pass around what data the user has updated
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
                btnClear.IsEnabled = true;

                //btnEdit.IsEnabled = false;
                //btnDelete.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Saves the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ///TO DO: PASS DATA AROUND
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
        /// Closes the Window without saving
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
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
            }
            catch
            {
                MessageBox.Show("Sorry, something went wrong!", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Clears 'Add new item' data fields for the user for re-entry. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = txtCost.Text = txtItemDescription.Text = "";
        }

    }//Def Window
}
