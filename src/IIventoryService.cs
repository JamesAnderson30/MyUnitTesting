using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_App1.src
{
    public interface IIventoryService
    {
        bool IsInStock(string sku, int qty);
        void Reserve(string sku, int qty);
    }
}
