using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    class Invoice {
        /// <summary>
        /// Invoice Identifier
        /// </summary>
        private int invoiceCode { get; set; }
        /// <summary>
        /// Invoice Date Created
        /// </summary>
        private DateTime invoiceDate { get; set; }

        //Getters and Setters
        public int InvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Invoice() {

        }
        
        

    }
}
