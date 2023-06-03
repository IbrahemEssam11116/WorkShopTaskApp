using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Bussniss.Services
{
    
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetAll();
        }
    }
}
