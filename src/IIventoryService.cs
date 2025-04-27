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
        void StockProduct(string sku, int qty);
        void SubtractProduct(string sku, int qty);
        void VoidReserve(string sku, int qty);
        void SubtractProductAndReserve(string sku, int qty);
        bool InventoryExists(string sku);
        bool ReserveExists(string sku);
    }
}
