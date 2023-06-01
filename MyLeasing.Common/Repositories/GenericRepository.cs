using MyLeasing.Common.Entities;


namespace MyLeasing.Common.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class, IEntity
{
    public IQueryable<T> GetAll()
    {
        
        throw new NotImplementedException();
    }


    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }


    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }


    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }


    public Task<bool> ExistAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task<bool> SaveAllAsync()
    {
        throw new NotImplementedException();
    }
}