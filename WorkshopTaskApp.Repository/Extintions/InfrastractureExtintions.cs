using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Repository.Data;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Repository.Extintions
{
    public class InfrastractureExtintions
    {
        public static void ConfigureDataBase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(b => b.UseSqlServer(connectionString));

        }
        public static void ConfigureServises(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
