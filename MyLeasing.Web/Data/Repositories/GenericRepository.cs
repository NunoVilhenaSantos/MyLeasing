using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.Repositories.Interfaces;

namespace MyLeasing.Web.Data.Repositories;

public class GenericRepository<T> :
    IGenericRepository<T> where T : class, IEntity
{
    private readonly DataContextMSSQL _dataContextMssql;


    protected GenericRepository(DataContextMSSQL dataContextMssql)
    {
        _dataContextMssql = dataContextMssql;
    }


    public IQueryable<T> GetAll()
    {
        return _dataContextMssql.Set<T>().AsQueryable().AsNoTracking();
    }


    public async Task<T> GetByIdAsync(int id)
    {
        // return await _dataContextMssql.Set<T>().FindAsync(id).AsTask();
        return await _dataContextMssql.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }


    public async Task<bool> CreateAsync(T entity)
    {
        await _dataContextMssql.Set<T>().AddAsync(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> UpdateAsync(T entity)
    {
        _dataContextMssql.Set<T>().Update(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> DeleteAsync(T entity)
    {
        _dataContextMssql.Set<T>().Remove(entity);
        return await SaveAllAsync();
    }


    public async Task<bool> ExistAsync(int id)
    {
        return await _dataContextMssql.Set<T>().AnyAsync(e => e.Id == id);
    }


    public async Task<bool> SaveAllAsync()
    {
        return await _dataContextMssql.SaveChangesAsync() > 0;
    }
}