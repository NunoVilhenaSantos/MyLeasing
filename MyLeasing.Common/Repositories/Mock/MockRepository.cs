using MyLeasing.Common.Entities;
using MyLeasing.Common.Repositories.OLD;

namespace MyLeasing.Common.Repositories.Mock;

public class MockRepository : IRepository
{
    public IEnumerable<Owner> GetOwners()
    {
        var owners = new List<Owner>
        {
            new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Document = "123456789",
                FixedPhone = "123456789",
                CellPhone = "123456789",
                Address = "Fake Street 123",
                User = new User
                {
                    Id = "123456789",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John.Doe@company.mail.com"
                }
            },

            new()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Document = "987654321",
                FixedPhone = "987654321",
                CellPhone = "987654321",
                Address = "Fake Street 321",
                User = new User
                {
                    Id = "987654321",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "Jane.Doe@company.mail.com"
                }
            },

            new()
            {
                Id = 3,
                FirstName = "John",
                LastName = "Smith",
                Document = "123456789",
                FixedPhone = "123456789",
                CellPhone = "123456789",
                Address = "Fake Street 123",
                User = new User
                {
                    Id = "123456789",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "John.Smith@company.mail.com"
                }
            },

            new()
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Smith",
                Document = "987654321",
                FixedPhone = "987654321",
                CellPhone = "987654321",
                Address = "Fake Street 321",
                User = new User
                {
                    Id = "987654321",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "Jane.Smith@company.mail.com"
                }
            },

            new()
            {
                Id = 5,
                FirstName = "Eagle",
                LastName = "Doe",
                Document = "123456789",
                FixedPhone = "123456789",
                CellPhone = "123456789",
                Address = "Fake Street 567",
                User = new User
                {
                    Id = "123456789",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = ""
                }
            }
        };

        return owners;
    }

    public Owner GetOwner(int id)
    {
        throw new NotImplementedException();
    }

    public void AddOwner(Owner owner)
    {
        throw new NotImplementedException();
    }

    public void UpdateOwner(Owner owner)
    {
        throw new NotImplementedException();
    }

    public void RemoveOwner(Owner owner)
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
}