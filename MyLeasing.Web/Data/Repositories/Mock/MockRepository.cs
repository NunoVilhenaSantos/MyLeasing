using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.Repositories.OLD;

namespace MyLeasing.Web.Data.Repositories.Mock;

public class MockRepository : IRepository
{
//    public IOrderedQueryable<Owner> GetOwners()
//    {
//        var owners = new List<Owner>
//        {
//            new Owner {1, "John", "Doe",
//                "123456789", "123456789",
//                "123456789", "Fake Street 123",
//                new User
//                {
//                    Id = "123456789",
//                    FirstName = "John",
//                    LastName = "Doe",
//                    Email = "John.Doe@company.mail.com"
//                }
//            },

//            new(2, "Jane", "Doe",
//                "987654321", "987654321",
//                "987654321", "Fake Street 321",
//                new User
//                {
//                    Id = "987654321",
//                    FirstName = "Jane",
//                    LastName = "Doe",
//                    Email = "Jane.Doe@company.mail.com"
//                })
//            {
//                ProfilePhotoUrl = string.Empty
//            },

//            new(3, "John", "Smith",
//                "123456789", "123456789",
//                "123456789", "Fake Street 123",
//                new User
//                {
//                    Id = "123456789",
//                    FirstName = "John",
//                    LastName = "Smith",
//                    Email = "John.Smith@company.mail.com"
//                })
//            {
//                ProfilePhotoUrl = string.Empty
//            },

//            new(4, "Jane", "Smith",
//                "987654321", "987654321",
//                "987654321", "Fake Street 321",
//                new User
//                {
//                    Id = "987654321",
//                    FirstName = "Jane",
//                    LastName = "Smith",
//                    Email = "Jane.Smith@company.mail.com"
//                })
//            {
//                ProfilePhotoUrl = string.Empty
//            },

//            new(5, "Eagle", "Doe",
//                "123456789", "123456789",
//                "123456789", "Fake Street 567",
//                new User
//                {
//                    Id = "123456789",
//                    FirstName = "John",
//                    LastName = "Doe",
//                    Email = ""
//                })
//            {
//                ProfilePhotoUrl = string.Empty
//            }
//        };


//        return owners.AsQueryable()
//            .OrderBy(o => o.FirstName)
//            .ThenBy(o => o.LastName);
//}


    public Owner? GetOwner(int id)
    {
        throw new NotImplementedException();
    }


    public void AddOwner(Owner? owner)
    {
        throw new NotImplementedException();
    }


    public void UpdateOwner(Owner? owner)
    {
        throw new NotImplementedException();
    }


    public void RemoveOwner(Owner? owner)
    {
        throw new NotImplementedException();
    }


    public Task<bool> SaveOwnersAsync()
    {
        throw new NotImplementedException();
    }


    public bool OwnerExists(int id)
    {
        throw new NotImplementedException();
    }

    public IOrderedQueryable<Owner?> GetOwners()
    {
        throw new NotImplementedException();
    }
}