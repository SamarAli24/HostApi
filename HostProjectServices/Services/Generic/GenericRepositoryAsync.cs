using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Services.Services
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region fields
        private readonly hosteduContext _dbContext;
        #endregion

        #region ctor
        public GenericRepositoryAsync(hosteduContext dbContext)
        {
            this._dbContext = dbContext;
        }
        #endregion

        #region methods
        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

      

       

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
            return entity;
        }


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            // await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public async Task CloseAsync(T entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public void CloseAll(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            //await _dbContext.SaveChangesAsync();
            return;
        }
        #endregion
    }
}
