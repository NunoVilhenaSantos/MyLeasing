using MyLeasing.Common.Entities;

namespace MyLeasing.Common.Repositories;

public interface IRepository
{
    IEnumerable<Owner> GetOwners();


    Owner GetOwner(int id);


    void AddOwner(Owner owner);


    void UpdateOwner(Owner owner);


    void RemoveOwner(Owner owner);


    Task<bool> SaveOwnersAsync();


    bool OwnerExists(int id);
}