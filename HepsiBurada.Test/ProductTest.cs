using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HepsiBurada.Test
{
    public class ProductTest
    {
        [Theory]
        [InlineData("P1", 100, 100, 0)]
        public void Is_ProductRepository_CreateProduct_Return_ProductInfo(string code, int price, int stock,
            int expectedResult)
        {
            TestStartup.ConfigureServices();

            var repository = TestStartup.ServiceProvider.GetService<IProductRepository>();
            var product = new Product
            {
                ProductCode = code, Price = price, Stock = stock
            };

            var result = repository.AddProduct(product);
            Assert.NotEqual(expectedResult, result.Id);

            TestStartup.DisposeServices();
        }

        [Theory]
        [InlineData("create_product P1 100 100", "Product created; code P1, price 100, stock 100")]
        public void Is_ProductService_CreateProduct_Return_ProductInfo(string command, string expectedResult)
        {
            TestStartup.ConfigureServices();

            var service = TestStartup.ServiceProvider.GetService<IProductService>();
            var result = service.CreateProduct(command.Split(' '));
            Assert.Equal(expectedResult, result);

            TestStartup.DisposeServices();
        }
        
        [Theory]
        [InlineData("create_product P1 100 100", "get_product_info P1",
            "Product P1 info; price 100, stock 100")]
        public void Is_ProductService_GetProductInfo_Return_ProductInfo(string createComman, string getCommand, string expectedResult)
        {
            TestStartup.ConfigureServices();
            
            var service = TestStartup.ServiceProvider.GetService<IProductService>();
            service.CreateProduct(createComman.Split(' '));
            var result = service.CreateProduct(getCommand.Split(' '));
            Assert.NotEqual(expectedResult, result);

            TestStartup.DisposeServices();
        }
    }
}