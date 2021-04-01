using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product FindByCode(string productCode);
    }
}