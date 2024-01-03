using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> AddEntity(T entity);
        Task<bool> UpdateEntity(T entity);
        Task<bool> DeleteEntity(object id);
        Task<bool> IsRecordExistsAsync(Expression<Func<T, bool>> condition);
    }
}
