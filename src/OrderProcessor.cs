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

        public OrderProcessor(IIventoryService inventoryService, IEmailService emailService)
        {
            _inventoryService = inventoryService;
            _emailService = emailService;
        }

        public void Process(Order order)
        {
            //TODO: Impliment validation, stock check, email send
        }
    }
}
