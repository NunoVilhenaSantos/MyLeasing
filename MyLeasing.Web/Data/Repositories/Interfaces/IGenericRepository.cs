using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetAll();


    Task<T> GetByIdAsync(int id);


    Task<bool> CreateAsync(T entity);


    Task<bool> UpdateAsync(T entity);


    Task<bool> DeleteAsync(T entity);


    Task<bool> ExistAsync(int id);


    Task<bool> SaveAllAsync();
}