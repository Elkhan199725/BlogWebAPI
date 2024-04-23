using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain.Models.Common;
using BlogApp.Application.Repositories.Interfaces;

namespace BlogApp.Persistence.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    private IDbContextTransaction _transaction;

    public GenericRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Apply includes
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        // Search for the entity with the given ID (assuming `Id` is the name of your primary key)
        return await query.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? page = null,
        int? pageSize = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        if (page != null && pageSize != null)
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

        return await query.ToListAsync();
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(predicate).ToListAsync();
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.SingleOrDefaultAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _dbSet.Add(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        _dbSet.AddRange(entities);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Transaction Methods
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
    }
}