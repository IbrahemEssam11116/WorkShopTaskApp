using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.DTOS;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Bussniss.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IGenericRepository<Product> genericRepository,
                              IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> AddProduct(Product product)
        {
            await _genericRepository.Add(product);
            var isSaved = await _unitOfWork.Save();
            return isSaved;
        }

        public async Task<List<Product>> GetProducts(int? categoryId = null)
        {
           
                var result = await _genericRepository.FindByCondition(p =>(categoryId == null || p.CategoryId == categoryId));
                return result.ToList();

            
        }
        public async Task<ProductDTO> GetProductsByPagination(int index,int row, int? categoryId = null)
        {

            ProductDTO returmModel = new();
            var products = await _genericRepository.FindByCondition(p => (categoryId == null || p.CategoryId == categoryId));
            returmModel.Products = products.ToList().OrderBy(p => p.Id).Skip((index - 1) * row).Take(row).ToList();
            double pageCount = (double)((double)products.Count() / (double)row);
            returmModel.PageCount = (int)Math.Ceiling(pageCount);

            returmModel.CurrentPageIndex = index;
            return returmModel;
        }


        public async Task<Product> Find(int id)
        {
            var fetchedProduct = await _genericRepository.GetById(id);
            return fetchedProduct;
        }

        public async Task<bool> Update(Product product)
        {
            _genericRepository.Update(product);
            var isUpdated = await _unitOfWork.Save();
            return isUpdated;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _genericRepository.GetById(id);
        }
    }
}
