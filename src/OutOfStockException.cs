using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_App1.src
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string sku, int qty)
            : base($"SKU '{sku}' is out of stock for quantity {qty}.")
        {

        }
    }
}
