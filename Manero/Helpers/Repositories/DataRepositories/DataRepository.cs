using Manero.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero.Helpers.Repositories.DataRepositories;

public abstract class DataRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    protected DataRepository(DataContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        if (entity != null)
            return entity;

        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();

    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int quantity)
    {
        return await _context.Set<TEntity>().Take(quantity).ToListAsync();

    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
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
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }

    }
}
