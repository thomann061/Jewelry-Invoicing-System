using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class InvoiceItem : ObservableObject {
        /// <summary>
        /// Invoice Item Code
        /// </summary>
        private int _invoiceItemCode;
        /// <summary>
        /// Item
        /// </summary>
        private Item _item;

        public Item Item {
            get {
                return _item;
            }
            set {
                if (value != _item) {
                    _item = value;
                    OnPropertyChanged("Item");
                }
            }
        }


        public int InvoiceItemCode {
            get {
                return _invoiceItemCode;
            }
            set {
                if (value != _invoiceItemCode) {
                    _invoiceItemCode = value;
                    OnPropertyChanged("InvoiceItemCode");
                }
            }
        }

    }
}
