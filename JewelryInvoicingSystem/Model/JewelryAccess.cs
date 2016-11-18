using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryInvoicingSystem.Model {
    class JewelryAccess {

    /// <summary>
    /// Database Access
    /// </summary>
    private DataAccess db;
    
    /// <summary>
    /// Default Constructor
    /// </summary>
    public JewelryAccess() {
            db = new DataAccess();
        }
    }
}
