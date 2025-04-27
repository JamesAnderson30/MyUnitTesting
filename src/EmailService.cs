using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_App1.src
{
    class EmailService : IEmailService
    {
        public void SendOrderConfirmation(string email, Order order)
        {
            //throws because it doesn't work!
            InvalidOperationException emailNotSentException = new InvalidOperationException("SMTP failure");
            throw emailNotSentException;
        }
    }
}
