using Microsoft.EntityFrameworkCore;
using MovieManagement.PresistentDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Data.EF
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;


        public BaseRepository(MovieManagementContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }



        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();

        }

        public async Task<T> GetAsync(params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(params object[] Key)
        {
            var entity = await GetAsync(Key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void SetState(T entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }

       
        public IQueryable<T> Table { get { return _dbSet; } }
        public IQueryable<T> TableNoTracking
        {
            get { return _dbSet.AsNoTracking(); }
        }


    }
}
