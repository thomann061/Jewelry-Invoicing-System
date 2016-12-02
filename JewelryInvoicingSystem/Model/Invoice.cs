using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class Invoice : INotifyPropertyChanged {
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
        /// Constructor
        /// </summary>
        public Invoice() {

        }

        /// <summary>
        /// String representation of an invoice
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return InvoiceCode.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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
