using JewelryInvoicingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.ViewModel {
    class SearchViewModel : ObservableObject {
        private ObservableCollection<Invoice> _invoices;
        private ObservableCollection<Invoice> _comboBoxInvoices;

        public SearchViewModel() {
            Invoices = new ObservableCollection<Invoice>();
            ComboBoxInvoices = new ObservableCollection<Invoice>();
        }

        public ObservableCollection<Invoice> Invoices {
            get { return _invoices; }
            set {
                if (value != _invoices) {
                    _invoices = value;
                    OnPropertyChanged("Invoices");
                }
            }
        }

        public ObservableCollection<Invoice> ComboBoxInvoices {
            get { return _comboBoxInvoices; }
            set {
                if (value != _comboBoxInvoices) {
                    _comboBoxInvoices = value;
                    OnPropertyChanged("ComboBoxInvoices");
                }
            }
        }
    }
}
