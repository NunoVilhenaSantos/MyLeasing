using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.Repositories.OLD;

public interface IRepository
{
    IOrderedQueryable<Owner> GetOwners();


    Owner GetOwner(int id);


    void AddOwner(Owner owner);


    void UpdateOwner(Owner owner);


    void RemoveOwner(Owner owner);


    Task<bool> SaveOwnersAsync();


    bool OwnerExists(int id);
}