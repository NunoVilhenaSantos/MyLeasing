using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.Repositories.OLD;

public class Repository : IRepository
{
    private readonly DataContextMSSQL _contextMssql;


    public Repository(DataContextMSSQL contextMssql)
    {
        _contextMssql = contextMssql;
    }


    public IOrderedQueryable<Owner> GetOwners()
    {
        return _contextMssql.Owners
            .OrderBy(o => o.FirstName)
            .ThenBy(o => o.LastName);
    }


    public Owner GetOwner(int id)
    {
        return _contextMssql.Owners.Find(id);
    }


    public void AddOwner(Owner owner)
    {
        _contextMssql.Owners.Add(owner);
    }


    public void UpdateOwner(Owner owner)
    {
        _contextMssql.Owners.Update(owner);
    }


    public void RemoveOwner(Owner owner)
    {
        _contextMssql.Owners.Remove(owner);
    }


    public async Task<bool> SaveOwnersAsync()
    {
        return await _contextMssql.SaveChangesAsync() > 0;
    }


    public bool OwnerExists(int id)
    {
        return _contextMssql.Owners.Any(o => o.Id == id);
    }
}