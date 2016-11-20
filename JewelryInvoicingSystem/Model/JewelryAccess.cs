using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    class JewelryAccess {

        /// <summary>
        /// Database Access
        /// </summary>
        private DataAccess db;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public JewelryAccess() {
            db = new DataAccess();
        }

        //Select Statements

        /// <summary>
        /// SQL Statement that selects all items
        /// </summary>
        /// <returns>An ObservableCollection of items</returns>
        public ObservableCollection<Item> selectItems() {
            ObservableCollection<Item> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT ItemCode, ItemName, ItemDesc, ItemCost FROM Item";
            col_Items = new ObservableCollection<Item>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Item objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Item item = new Item();

                    item.ItemCode = int.Parse(ds.Tables[0].Rows[i]["ItemCode"].ToString());
                    item.ItemName = ds.Tables[0].Rows[i]["ItemName"].ToString();
                    item.ItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    item.ItemCost = double.Parse(ds.Tables[0].Rows[i]["ItemCost"].ToString());

                    col_Items.Add(item);
                }
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        /// <summary>
        /// SQL Statement that selects an item by id
        /// </summary>
        /// <returns>An ObservableCollection of items</returns>
        public ObservableCollection<Item> selectItemById(int id) {
            ObservableCollection<Item> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT ItemCode, ItemName, ItemDesc, ItemCost FROM Item WHERE ItemCode = " + id;
            col_Items = new ObservableCollection<Item>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Item objects based on the data pulled from the query
                Item item = new Item();

                item.ItemCode = int.Parse(ds.Tables[0].Rows[0]["ItemCode"].ToString());
                item.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                item.ItemDesc = ds.Tables[0].Rows[0]["ItemDesc"].ToString();
                item.ItemCost = double.Parse(ds.Tables[0].Rows[0]["ItemCost"].ToString());

                col_Items.Add(item);
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        /// <summary>
        /// SQL Statement that selects all invoices
        /// </summary>
        /// <returns>An ObservableCollection of Invoices</returns>
        public ObservableCollection<Invoice> selectInvoices() {
            ObservableCollection<Invoice> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT InvoiceCode, InvoiceDate FROM Invoice";
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[0]["InvoiceCode"].ToString());
                    invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[0]["InvoiceDate"].ToString());

                    col_Items.Add(invoice);
                }
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        /// <summary>
        /// SQL Statement that selects an invoice by id
        /// </summary>
        /// <returns>An ObservableCollection of Invoices</returns>
        public ObservableCollection<Invoice> selectInvoiceById(int id) {
            ObservableCollection<Invoice> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT InvoiceCode, InvoiceDate FROM Invoice WHERE InvoiceCode = " + id;
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                Invoice invoice = new Invoice();

                invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[0]["InvoiceCode"].ToString());
                invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[0]["InvoiceDate"].ToString());

                col_Items.Add(invoice);
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        //End Select Statements

        //Insert Statements

        /// <summary>
        /// SQL Statement that inserts an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool insertItem(Item item) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "INSERT INTO Item (ItemName, ItemDesc, ItemCost) " +
                   "VALUES(" + item.ItemName + ", " + item.ItemDesc + ", " + item.ItemCost + ")";
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        /// <summary>
        /// SQL Statement that inserts an invoice
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool insertInvoice(Invoice invoice) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "INSERT INTO Invoice (InvoiceDate) " +
                   "VALUES(" + invoice.InvoiceDate + ")";
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        //End Insert Statements

        //Update Statements

        /// <summary>
        /// SQL Statement that updates an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool updateItem(Item item) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "UPDATE Item " +
                   "SET ItemName=" + item.ItemName + ", ItemDesc=" + item.ItemDesc + ", ItemCost=" + item.ItemCost + " " +
                   "WEHERE ItemCode = " + item.ItemCode;
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch(Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        /// <summary>
        /// SQL Statement that updates an invoice
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool updateInvoice(Invoice invoice) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "UPDATE Invoice " +
                   "SET InvoiceDate=" + invoice.InvoiceDate + " " +
                   "WEHERE InvoiceCode = " + invoice.InvoiceCode;
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch(Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        //End Update Statements

        //Delete Statements

        /// <summary>
        /// SQL Statement that deletes an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool deleteItem(int id) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "DELETE FROM Item " +
                   "WEHERE ItemCode = " + id;
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        /// <summary>
        /// SQL Statement that deletes an invoice
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool deleteInvoice(int id) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "DELETE FROM Invoice " +
                   "WEHERE InvoiceCode = " + id;
            try {
                rowCount = db.ExecuteNonQuery(sSQL);
                //if insert unsuccessful
                if (rowCount == 0)
                    return false;
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return true;
        }

        //End Delete Statements
    }
}
