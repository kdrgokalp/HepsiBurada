
namespace HepsiBurada.Services.Interfaces
{
    public interface IProductService
    {
        string CreateProduct(string[] commandLine);
        string GetProductInfo(string[] commandLine);
    }
}