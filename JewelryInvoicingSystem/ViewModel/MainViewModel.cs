using JewelryInvoicingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.ViewModel {
    class MainViewModel : ObservableObject {
        private ObservableCollection<Item> _items;
        private Invoice _invoice;
        private ObservableCollection<InvoiceItem> _invoiceItems;

        public MainViewModel() {
            Items = new ObservableCollection<Item>();
            InvoiceItems = new ObservableCollection<InvoiceItem>();
            Invoice = new Invoice();
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

        public Invoice Invoice {
            get { return _invoice; }
            set {
                if (value != _invoice) {
                    _invoice = value;
                    OnPropertyChanged("Invoice");
                }
            }
        }

        public ObservableCollection<InvoiceItem> InvoiceItems {
            get { return _invoiceItems; }
            set {
                if (value != _invoiceItems) {
                    _invoiceItems = value;
                    OnPropertyChanged("InvoiceItems");
                }
            }
        }
    }
}
