using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class InvoiceItem : INotifyPropertyChanged {
        /// <summary>
        /// Item Cost
        /// </summary>
        private double _itemCost;
        /// <summary>
        /// Item
        /// </summary>
        private Item _item;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public double ItemCost {
            get {
                return _itemCost;
            }
            set {
                if (value != _itemCost) {
                    _itemCost = value;
                    OnPropertyChanged("ItemCost");
                }
            }
        }

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
