﻿using Manero.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero.Helpers.Repositories.UserRepositories;

public interface IRepo<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
{
    
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();   
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
}

public abstract class IdRepository<TEntity, TDbContext> : IRepo<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
{
    private readonly IdentityContext _context;

    public IdRepository(IdentityContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        if (entity != null)
            return entity;

        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().Where(expression).ToListAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { };

        return false;
    }
}