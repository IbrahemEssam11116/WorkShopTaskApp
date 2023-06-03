using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Bussniss.Services;

namespace WorkshopTaskApp.Bussniss.BussiunessExtintions
{
    public static class BussiunessExtintions
    {
        public static void ConfigureServises(IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
