using System.Linq;
using HepsiBurada.Data.Context;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly HepsiBuradaContext _context;

        public ProductRepository(HepsiBuradaContext context) => _context = context;

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product FindByCode(string productCode)
        {
            return _context.Products.FirstOrDefault(x => x.ProductCode.ToLower().Contains(productCode.ToLower()));
        }
    }
}