using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Repository.Data;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} Couldn't be Added : {ex.Message}");
            }

        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't Retrieve Data:{ex.Message}");
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't Retrieve Data:{ex.Message}");
            }
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (expression != null)
                    query = query.Where(expression);

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't Retrieve Data:{ex.Message}");
            }
        }

        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't Retrieve Data:{ex.Message}");
            }
        }

    }
}
