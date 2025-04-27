using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_App1.src
{
    public interface IEmailService
    {
        void SendOrderConfirmation(string email, Order order);
    }
}
