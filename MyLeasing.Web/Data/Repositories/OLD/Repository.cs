using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data.Repositories.OLD;

public class Repository : IRepository
{
    private readonly DataContext _context;


    public Repository(DataContext context)
    {
        _context = context;
    }


    public IOrderedQueryable<Owner> GetOwners()
    {
        return _context.Owners
            .OrderBy(o => o.FirstName)
            .ThenBy(o => o.LastName);
    }


    public Owner? GetOwner(int id)
    {
        return _context.Owners.Find(id);
    }


    public void AddOwner(Owner? owner)
    {
        _context.Owners.Add(owner);
    }


    public void UpdateOwner(Owner? owner)
    {
        _context.Owners.Update(owner);
    }


    public void RemoveOwner(Owner? owner)
    {
        _context.Owners.Remove(owner);
    }


    public async Task<bool> SaveOwnersAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }


    public bool OwnerExists(int id)
    {
        return _context.Owners.Any(o => o.Id == id);
    }
}