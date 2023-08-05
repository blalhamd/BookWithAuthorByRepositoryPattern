
using Microsoft.EntityFrameworkCore;
using Repositories.APPDPCONTEXT;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositories.EntitiesRepositories
{
    public class GenericRepository<T> : IGenricReopsitories<T> where T : class
    {
        
         private AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var query= await _context.Set<T>().ToListAsync();

            return query;
        }


        public async Task<T> GetById(object id)
        {
            var search = await _context.Set<T>().FindAsync(id);

            return search;
        }


        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> match)
        {
            var search = await _context.Set<T>().Where(match).ToListAsync();

            return search;
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);

        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);

        }


        public void Update(T entity)
        {
            _context.Update(entity);

        }

        public void Delete(T entity)
        {

            _context.Remove(entity);
        }


        public void save()
        {
            _context.SaveChanges();
        }

        
    }
}
