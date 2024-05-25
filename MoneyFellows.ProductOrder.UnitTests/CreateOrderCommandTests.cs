using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Commands.CreateOrderCommand;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;
using Moq;

namespace MoneyFellows.ProductOrder.Tests
{
    public class CreateOrderCommandTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldAddOrder()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                DeliveryAddress = "123 Test St",
                TotalCost = 100.0m,
                OrderDetails = new List<CreateOrderCommandOrderDetail>
            {
                new CreateOrderCommandOrderDetail { ProductId = 1, Quantity = 2 }
            },
                Customer = new CreateOrderCommandCustomer
                {
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    ContactNumber = "1234567890"
                },
                DeliveryTime = DateTime.UtcNow
            };

            _productRepositoryMock
                .Setup(repo => repo.AreProductsExistAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(true);

            _mapperMock
                .Setup(mapper => mapper.Map<Order>(command))
                .Returns(new Order());

            _orderRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _orderRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                DeliveryAddress = "123 Test St",
                TotalCost = 100.0m,
                OrderDetails = new List<CreateOrderCommandOrderDetail>
            {
                new CreateOrderCommandOrderDetail { ProductId = 1, Quantity = 2 }
            },
                Customer = new CreateOrderCommandCustomer
                {
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    ContactNumber = "1234567890"
                },
                DeliveryTime = DateTime.UtcNow
            };

            _productRepositoryMock
                .Setup(repo => repo.AreProductsExistAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
