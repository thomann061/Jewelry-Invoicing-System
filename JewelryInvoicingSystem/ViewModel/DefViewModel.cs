using JewelryInvoicingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.ViewModel {
    class DefViewModel : ObservableObject {
        private ObservableCollection<Item> _items;

        public DefViewModel() {
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items {
            get { return _items; }
            set {
                if (value != _items) {
                    _items = value;
                    OnPropertyChanged("Items");
                }
            }
        }
    }
}
