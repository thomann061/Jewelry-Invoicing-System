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
        private DateTime _invoiceDate;

        /// <summary>
        /// Constructor
        /// </summary>
        public Invoice() {

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
        public DateTime InvoiceDate {
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
