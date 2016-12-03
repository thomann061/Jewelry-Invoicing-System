﻿using System;
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

        #region Select Statements

        /// <summary>
        /// SQL Statement that selects all items
        /// </summary>
        /// <returns>An ObservableCollection of items</returns>
        public ObservableCollection<Item> selectItems() {
            ObservableCollection<Item> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT * FROM Item";
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
            Console.WriteLine(col_Items.Count);
            return col_Items;
        }

        /// <summary>
        /// Selects the next id for the invoice
        /// </summary>
        /// <returns></returns>
        public int selectNextInvoiceId() {
            string idSQL;    //Holds an SQL statement
            int newId = 0;
            idSQL = "SELECT TOP 1 InvoiceCode FROM Invoice ORDER BY InvoiceCode DESC;";
            try {
                newId = int.Parse(db.ExecuteScalarSQL(idSQL));
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //if successful
            return ++newId;
        }

        /// <summary>
        /// SQL Statement that selects all items from an invoice
        /// </summary>
        /// <returns>An ObservableCollection of InvoiceItems</returns>
        public ObservableCollection<InvoiceItem> selectItemsFromInvoice(int id) {
            ObservableCollection<InvoiceItem> col_Items;
            string sSQL;    //Holds an SQL statement
            string s2SQL;
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT ItemCode, InvoiceItemCode FROM InvoiceItem WHERE InvoiceCode = " + id + ";";
            col_Items = new ObservableCollection<InvoiceItem>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Item objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    InvoiceItem invoiceItem = new InvoiceItem();
                    Item item = new Item();

                    invoiceItem.InvoiceItemCode = int.Parse(ds.Tables[0].Rows[i]["InvoiceItemCode"].ToString());
                    int itemCode = int.Parse(ds.Tables[0].Rows[i]["ItemCode"].ToString());

                    s2SQL = "SELECT ItemName, ItemCost, ItemDesc FROM Item WHERE ItemCode = " + itemCode + ";";
                    int ret = 0;
                    DataSet ds2 = db.ExecuteSQLStatement(s2SQL, ref ret);

                    item.ItemName = ds2.Tables[0].Rows[0]["ItemName"].ToString();
                    item.ItemDesc = ds2.Tables[0].Rows[0]["ItemDesc"].ToString();
                    item.ItemCost = double.Parse(ds2.Tables[0].Rows[0]["ItemCost"].ToString());
                    item.ItemCode = itemCode;

                    invoiceItem.Item = item;

                    col_Items.Add(invoiceItem);
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
            sSQL = "SELECT ItemCode, ItemName, ItemCost FROM Item WHERE ItemCode = " + id;
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
        /// SQL Statement that selects all invoices by Id
        /// </summary>
        /// <returns>An ObservableCollection of Invoices</returns>
        public ObservableCollection<Invoice> selectInvoices() {
            ObservableCollection<Invoice> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT * FROM Invoice";
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[i]["InvoiceCode"].ToString());
                    invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    invoice.InvoiceTotal = double.Parse(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString());

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
            sSQL = "SELECT * FROM Invoice WHERE InvoiceCode = " + id + ";";
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[i]["InvoiceCode"].ToString());
                    invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    invoice.InvoiceTotal = double.Parse(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString());

                    col_Items.Add(invoice);
                }
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        /// <summary>
        /// SQL Statement that selects an invoice by date
        /// </summary>
        /// <returns>An ObservableCollection of Invoices</returns>
        public ObservableCollection<Invoice> selectInvoicesByDate(string date) {
            ObservableCollection<Invoice> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT * FROM Invoice WHERE InvoiceDate = #" + date + "#;";
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[i]["InvoiceCode"].ToString());
                    invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    invoice.InvoiceTotal = double.Parse(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString());

                    col_Items.Add(invoice);
                }
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        /// <summary>
        /// SQL Statement that selects an invoice by total
        /// </summary>
        /// <returns>An ObservableCollection of Invoices</returns>
        public ObservableCollection<Invoice> selectInvoicesByTotal(double total) {
            ObservableCollection<Invoice> col_Items;
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            sSQL = "SELECT * FROM Invoice WHERE InvoiceTotal <= " + total + ";";
            col_Items = new ObservableCollection<Invoice>();
            try {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates Invoice objects based on the data pulled from the query
                for (int i = 0; i < iRet; i++) {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceCode = int.Parse(ds.Tables[0].Rows[i]["InvoiceCode"].ToString());
                    invoice.InvoiceDate = DateTime.Parse(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    invoice.InvoiceTotal = double.Parse(ds.Tables[0].Rows[i]["InvoiceTotal"].ToString());

                    col_Items.Add(invoice);
                }
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return col_Items;
        }

        #endregion

        #region Insert Statements

        /// <summary>
        /// SQL Statement that inserts an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool insertItem(Item item) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "INSERT INTO Item (ItemName, ItemDesc, ItemCost) " +
                   "VALUES('" + item.ItemName + "', " + "'" + item.ItemDesc + "', " + item.ItemCost + ");";
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
        /// <returns>if success true, otherwise false</returns>
        public bool insertInvoice(Invoice invoice) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "INSERT INTO Invoice (InvoiceDate) " +
                   "VALUES('" + invoice.InvoiceDate + "');";
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

        #endregion

        #region Update Statements

        /// <summary>
        /// SQL Statement that updates an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool updateItem(Item item) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "UPDATE Item " +
                   "SET ItemName='" + item.ItemName + "', ItemCost=" + item.ItemCost + ", ItemDesc='" + item.ItemDesc + "' " +
                   "WHERE ItemCode = " + item.ItemCode;
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
        /// SQL Statement that inserts an invoice item
        /// </summary>
        /// <returns>if success true, otherwise false</returns>
        public bool insertInvoiceItem(InvoiceItem invoiceItem, Invoice invoice) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "INSERT INTO InvoiceItem (ItemCode, InvoiceCode) " +
                   "VALUES(" + invoiceItem.Item.ItemCode + ", " + invoice.InvoiceCode + ");";
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
        /// SQL Statement that updates an invoice and its items
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool updateInvoiceWithItems(Invoice invoice, ObservableCollection<InvoiceItem> invoiceItems) {
            string sSQL;    //Holds an SQL statement
            string s2SQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            try {
                //store total
                double total = 0;

                //insert items
                foreach (InvoiceItem el in invoiceItems) {
                    s2SQL = "INSERT INTO InvoiceItem (ItemCode, InvoiceCode)" +
                            "VALUES(" + el.Item.ItemCode + ", " + invoice.InvoiceCode + ");";
                    rowCount = db.ExecuteNonQuery(s2SQL);
                    total += el.Item.ItemCost; //add up total
                    //if insert unsuccessful
                    if (rowCount == 0)
                        return false;
                }
                //execute Invoice
                sSQL = "UPDATE Invoice " +
                       "SET InvoiceDate='" + invoice.InvoiceDate + "', InvoiceTotal= " + total + " " +
                       "WHERE InvoiceCode = " + invoice.InvoiceCode + ";";
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

        #endregion

        #region Delete Statements

        /// <summary>
        /// SQL Statement that deletes an item
        /// </summary>
        /// <returns>true if successful or false if unsuccessful</returns>
        public bool deleteItem(int id) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            string iSQL = "SELECT TOP 1 InvoiceItemCode FROM InvoiceItem WHERE ItemCode = " + id + ";";
            sSQL = "DELETE FROM Item WHERE ItemCode = " + id + ";";
            try {
                //first check if Item is in use.
                rowCount = db.ExecuteNonQuery(iSQL);
                if (rowCount == 0)
                    return false;
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
        public bool deleteInvoice(Invoice invoice) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "DELETE FROM Invoice " +
                   "WHERE InvoiceCode = " + invoice.InvoiceCode + ";";
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
        public bool deleteInvoiceItem(InvoiceItem invoiceItem) {
            string sSQL;    //Holds an SQL statement
            int rowCount = 0;   //Number of rows Affected
            sSQL = "DELETE FROM InvoiceItem " +
                   "WHERE InvoiceItemCode = " + invoiceItem.InvoiceItemCode + ";";
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

        #endregion
    }
}
