using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Order_App1.src
{
    public class OrderProcessor
    {
        private readonly IIventoryService _inventoryService;
        private readonly IEmailService _emailService;
        private readonly InventoryService InventoryService = new InventoryService();
        private readonly EmailService EmailService = new EmailService();
        public OrderProcessor(IIventoryService inventoryService, IEmailService emailService)
        {
            _inventoryService = inventoryService;
            _emailService = emailService;

           //Init test stocks
           
        }

        public void Process(Order order)
        {
            //Validation
            if(order is null)
            {
                Exception nullArgumentException = new ArgumentNullException("Order");
                throw nullArgumentException;
            } else if(order.Quantity == 0)
            {
                Exception argumentException = new ArgumentException("Quantity of zero should be invalid");
                throw argumentException;
            } else if(!InventoryService.IsInStock(order.Sku, order.Quantity))
            {
                OutOfStockException outOfStockException = new OutOfStockException(order.Sku, order.Quantity);
                throw outOfStockException;
            } else
            {
                InventoryService.Reserve(order.Sku, order.Quantity);
                EmailService.SendOrderConfirmation(order.CustomerEmail, order);
            }
        }
    }
}
