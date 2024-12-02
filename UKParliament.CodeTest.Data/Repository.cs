using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly PersonManagerContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(PersonManagerContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> CreateAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);

            var isSuccess = await _context.SaveChangesAsync() > 0;

            return isSuccess;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create entity", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update entity", ex);
        }
    }
}