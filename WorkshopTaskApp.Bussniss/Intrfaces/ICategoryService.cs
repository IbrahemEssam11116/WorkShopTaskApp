using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Entity.Models;

namespace WorkshopTaskApp.Bussniss.Intrfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}
