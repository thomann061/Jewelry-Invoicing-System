using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    public class Item {
        /// <summary>
        /// Item identifier
        /// </summary>
        private int itemCode;
        /// <summary>
        /// Item Name
        /// </summary>
        private string itemName;
        /// <summary>
        /// Item description
        /// </summary>
        private string itemDesc;
        /// <summary>
        /// Item Price
        /// </summary>
        private double itemCost;

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
                return this.ItemDesc + "$" + this.ItemCost;
            } catch (Exception) {
                throw;
            }
        }

        //getters and setters
        public int ItemCode {
            get {
                return itemCode;
            }

            set {
                itemCode = value;
            }
        }

        public string ItemName {
            get {
                return itemName;
            }

            set {
                itemName = value;
            }
        }

        public string ItemDesc {
            get {
                return itemDesc;
            }

            set {
                itemDesc = value;
            }
        }

        public double ItemCost {
            get {
                return itemCost;
            }

            set {
                itemCost = value;
            }
        }
    }
}
