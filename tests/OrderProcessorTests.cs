using System;
using System.ComponentModel;
using Moq;
using NUnit.Framework;
using Order_App1.src;

namespace Order_App1.tests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private Mock<IIventoryService> _invMock;
        private Mock<IEmailService> _emailMock;
        private OrderProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _invMock = new Mock<IIventoryService>();
            _emailMock = new Mock<IEmailService>();
            _processor = new OrderProcessor(_invMock.Object, _emailMock.Object);
        }

        [Test]
        public void Process_NullOrder_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _processor.Process(null));
        }

        [Test]
        public void Process_QuantityZero_ThrowsArgumentException()
        {
            var order = new Order { Sku = "TEST", Quantity = 0, CustomerEmail = "Me@me.com"};
            Assert.Throws<ArgumentException>(() => _processor.Process(order),
                "Quantity of zero should be invalid");
        }
        [Test]
        public void Process_OutOfStock_ThrowsOutOfStockException()
        {
            var order = new Order { Sku = "XYZ", Quantity = 5, CustomerEmail = "you@here.com" };
            _invMock.Setup(i => i.IsInStock(order.Sku, order.Quantity)).Returns(false);

            Assert.Throws<OutOfStockException>(() => _processor.Process(order));
        }

        [Test]
        public void Process_OutOfStock_DoesNotReserveOrSendEmail()
        {
            var order = new Order { Sku = "XYZ", Quantity = 5, CustomerEmail = "you@here.com" };
            _invMock.Setup(i => i.IsInStock(order.Sku, order.Quantity)).Returns(false);

            Assert.Throws<OutOfStockException>(() => _processor.Process(order));

            _invMock.Verify(i => i.Reserve(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _emailMock.Verify(m => m.SendOrderConfirmation(It.IsAny<string>(), It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void Process_InStock_ReservesAndSendsEmail()
        {
            var order = new Order { Sku = "ABC", Quantity = 2, CustomerEmail = "test@x.com" };
            _invMock.Setup(i => i.IsInStock(order.Sku, order.Quantity)).Returns(true);

            _processor.Process(order);

            _invMock.Verify(i => i.Reserve(order.Sku, order.Quantity), Times.Once);
            _emailMock.Verify(m => m.SendOrderConfirmation(order.CustomerEmail, order), Times.Once);
        }

        [Test]
        public void Process_EmailServiceThrows_ExceptionIsPropagated()
        {
            var order = new Order { Sku = "ABC", Quantity = 2, CustomerEmail = "test@x.com" };
            _invMock.Setup(i => i.IsInStock(order.Sku, order.Quantity)).Returns(true);
            _emailMock
                .Setup(m => m.SendOrderConfirmation(order.CustomerEmail, order))
                .Throws(new InvalidOperationException("SMTP failure"));

            var ex = Assert.Throws<InvalidOperationException>(() => _processor.Process(order));
            Assert.That(ex.Message, Is.EqualTo("SMTP failure"));
        }
    }
}
