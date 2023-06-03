using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Entity.Models;

namespace WorkshopTaskApp.Bussniss.DTOS
{
    public class ProductDTO
    {
        public List<Product> Products { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int? CategoryId { get; set; }
    }
}
