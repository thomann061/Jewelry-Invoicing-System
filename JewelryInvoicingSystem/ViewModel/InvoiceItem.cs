using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class InvoiceItem : ObservableObject {
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

    }
}
