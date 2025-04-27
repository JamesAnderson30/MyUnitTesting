using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_App1.src
{
    class InventoryService : IIventoryService
    {
        public InventoryService()
        {
            this.inventory["XYZ"] = 10;
            this.inventory["ABC"] = 3;
        }
        //Init Test Inventory
        private Dictionary<string, int> inventory = new Dictionary<string, int>();
        private Dictionary<string, int> reservedInventory = new Dictionary<string, int>();
        //Note: Inventory holds the total inventory. Reserves just subtracts a certain qty from stock checks
        //for the purposes of making a new purchase. 

        

        public bool InventoryExists(string sku)
        {
            if (inventory.ContainsKey(sku))
            {
                return true;
            }
            return false;
        }

        public bool ReserveExists(string sku)
        {
            if (reservedInventory.ContainsKey(sku))
            {
                return true;
            }
            return false;
        } 

        public bool IsInStock(string sku, int qty)
        {
            if (!InventoryExists(sku))
            {
                OutOfStockException outOfStockException = new OutOfStockException(sku, qty);
                throw outOfStockException;
                return false;
            }
            if (inventory[sku] - (ReserveExists(sku) ? reservedInventory[sku] : 0) >= qty)
            {
                return true;
            }
            return false;
        }

        public void Reserve(string sku, int qty)
        {
            if (!InventoryExists(sku))
            {
                Exception argumentException = new ArgumentException("Cannot subtract inventory from SKU that doesn't exist");
                throw argumentException;
            } else if(!IsInStock(sku, qty))
            {
                OutOfStockException outOfStockException = new OutOfStockException(sku, qty);
                throw outOfStockException;
            } else
            {
                if (ReserveExists(sku))
                {
                    reservedInventory[sku] += qty;
                } else
                {
                    reservedInventory[sku] = qty;
                }
            }
        }

        public void StockProduct(string sku, int qty)
        {
            if (inventory.ContainsKey(sku))
            {
                inventory[sku] += qty;
            } else
            {
                inventory[sku] = qty;
            }
        }

        public void SubtractProduct(string sku, int qty)
        {
            if (!InventoryExists(sku))
            {
                Exception argumentException = new ArgumentException("Cannot subtract inventory from SKU that doesn't exist");
                throw argumentException;
            } else if(!IsInStock(sku, qty))
            {
                OutOfStockException outOfStockException = new OutOfStockException(sku, qty);
                throw outOfStockException;
            }
            {
                inventory[sku] -= qty;
            }
        }

        public void SubtractProductAndReserve(string sku, int qty)
        {
            //If inventory or reserve does not contain the SKU
            if (!InventoryExists(sku))
            {
                Exception argumentException = new ArgumentException("Cannot subtract inventory from SKU that doesn't exist");
                throw argumentException;
            } else if (!ReserveExists(sku))
            {
                Exception argumentException = new ArgumentException("Cannot subtract inventory from SKU that doesn't exist");
                throw argumentException;
            } else
            {
                inventory[sku] -= qty;
                reservedInventory[sku] -= qty;
            }
        }

        public void VoidReserve(string sku, int qty)
        {
            if (!ReserveExists(sku))
            {
                Exception argumentException = new ArgumentException("Cannot subtract inventory from SKU that doesn't exist");
                throw argumentException;
            }
            else
            {
                reservedInventory[sku] -= qty;
            }
        }
    }
}
