using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class Invoice : ObservableObject {
        /// <summary>
        /// Invoice Identifier
        /// </summary>
        private int _invoiceCode;
        /// <summary>
        /// Invoice Date Created
        /// </summary>
        private DateTime? _invoiceDate;
        /// <summary>
        /// Invoice Total Created
        /// </summary>
        private double _invoiceTotal;

        /// <summary>
        /// String representation of an invoice
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return InvoiceCode.ToString();
        }

        //getters and setters
        public int InvoiceCode {
            get {
                return _invoiceCode;
            }
            set {
                if (value != _invoiceCode) {
                    _invoiceCode = value;
                    OnPropertyChanged("InvoiceCode");
                }
            }
        }

        //getters and setters
        public double InvoiceTotal {
            get {
                return _invoiceTotal;
            }
            set {
                if (value != _invoiceTotal) {
                    _invoiceTotal = value;
                    OnPropertyChanged("InvoiceTotal");
                }
            }
        }

        //getters and setters
        public DateTime? InvoiceDate {
            get {
                return _invoiceDate;
            }
            set {
                if (value != _invoiceDate) {
                    _invoiceDate = value;
                    OnPropertyChanged("InvoiceDate");
                }
            }
        }

        

    }
}
