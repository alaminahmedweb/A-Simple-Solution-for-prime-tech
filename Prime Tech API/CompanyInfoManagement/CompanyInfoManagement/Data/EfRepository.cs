using Application.Common.Interfaces;
using CompanyInfoManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        internal DbSet<T> _dbSet { get; set; }

        public EfRepository(AppDbContext dbContext) {
            this._dbContext = dbContext;
            this._dbSet= dbContext.Set<T>();
        }

        public async Task<IEnumerable> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await this._dbSet.FindAsync(id);
        }
        public async Task<T> AddEntity(T entity)
        {
            try
            {
                await this._dbSet.AddAsync(entity);
                return entity;
            }
            catch (Exception ex) {
                return entity;
            }
        }

        public async Task<bool> DeleteEntity(object id)
        {
            T entity = await this._dbSet.FindAsync(id);

            if(entity == null)
            {
                return false;
            }
            else
            {
                _dbSet.Remove(entity);
                return true;
            }
        }


        public Task<bool> IsRecordExistsAsync(Expression<Func<T, bool>> condition)
        {
            return _dbContext.Set<T>().AnyAsync(condition);
        }

        public  async Task<bool> UpdateEntity(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
