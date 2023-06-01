using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.DataContexts;
using MyLeasing.Common.Entities;

namespace MyLeasing.Common.Repositories;

public class GenericRepository<T> :
    IGenericRepository<T> where T : class, IEntity
{
    private readonly DataContext _dataContext;


    protected GenericRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public IQueryable<T> GetAll()
    {
        return _dataContext.Set<T>().AsQueryable().AsNoTracking();
    }


    public async Task<T?> GetByIdAsync(int id)
    {
        // return await _dataContext.Set<T>().FindAsync(id).AsTask();
        return await _dataContext.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }


    public async Task<bool> CreateAsync(T entity)
    {
        await _dataContext.Set<T>().AddAsync(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> UpdateAsync(T entity)
    {
        _dataContext.Set<T>().Update(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> DeleteAsync(T entity)
    {
        _dataContext.Set<T>().Remove(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> ExistAsync(int id)
    {
        return await _dataContext.Set<T>().AnyAsync(e => e.Id == id);
    }


    public async Task<bool> SaveAllAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }
}