using Application.Common.Interfaces;
using CompanyInfoManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IRepository<Company> repository,IUnitOfWork unitOfWork) {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Company> AddEntity(Company entity)
        {
            await _repository.AddEntity(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Company> GetByIdAsync(object id)
        {
           return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<Company, bool>> condition)
        {
            return await _repository.IsRecordExistsAsync(condition);
        }

        public async Task<bool> UpdateEntity(Company entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
