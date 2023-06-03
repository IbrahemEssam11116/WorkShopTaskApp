using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.DTOS;
using WorkshopTaskApp.Entity.Models;

namespace WorkshopTaskApp.Bussniss.Intrfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(Product product);
        Task<List<Product>> GetProducts(int? categoryId = null);
        Task<ProductDTO> GetProductsByPagination(int index, int row, int? categoryId = null);
        Task<Product> Find(int id);
        Task<bool> Update(Product product);
        Task<Product> GetProductById(int id);
    }
}
