using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class Item : INotifyPropertyChanged {
        /// <summary>
        /// Item identifier
        /// </summary>
        private int _itemCode;
        /// <summary>
        /// Item Name
        /// </summary>
        private string _itemName;

        /// <summary>
        /// Constructor for an item
        /// </summary>
        public Item() {

        }

        /// <summary>
        /// String representation of an Item
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            try {
                return this.ItemName;
            } catch (Exception) {
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //getters and setters
        public int ItemCode {
            get {
                return _itemCode;
            }
            set {
                if(value != _itemCode) {
                    _itemCode = value;
                    OnPropertyChanged("ItemCode");
                }
            }
        }

        public string ItemName {
            get {
                return _itemName;
            }
            set {
                if (value != _itemName) {
                    _itemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }

    }
}
