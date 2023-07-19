using Application.Services.Interfaces;
using AutoFixture;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Test.TestUtils;

namespace Test
{
    public class ProductTest
    {
        private readonly Fixture fixture = new();
        private readonly Mock<IUnitOfWork> unitOfWork = new();
        private readonly Mock<IDistributedCache> cache = new();
        private readonly MockRepository<Product> productRepository = new();
        private ProductService productService;
        private ICachingService<Product> cachingService;
        public ProductTest() 
        {
            unitOfWork.Setup(uow => uow.productRepository).Returns(productRepository);
            cachingService = new CachingService<Product>(cache.Object);
            productService = new(cachingService, unitOfWork.Object);
        }

        private Product CreateProduct()
        { 
            return fixture.Build<Product>()
                .With(x => x.Id, Utils.GenerateId())
                .With(x => x.Name, "Test Product")
                .With(x => x.Quantity,1)
                .With(x => x.Description, "Test Description")
                .Create();
        }

        [Fact]
        public async void Create_Product_Success()
        { 
            var product = CreateProduct();
            var response = await productService.Insert(product);
            Assert.Equal(product.Id, response.Id);
        }
    }
}
