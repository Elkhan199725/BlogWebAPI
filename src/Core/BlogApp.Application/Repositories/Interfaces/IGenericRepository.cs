using BlogApp.Domain.Models.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> filter,
                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int? page = null,
        int? pageSize = null);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}