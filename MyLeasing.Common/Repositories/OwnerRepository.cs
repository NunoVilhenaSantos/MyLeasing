using MyLeasing.Common.DataContexts;
using MyLeasing.Common.Entities;
using MyLeasing.Common.Repositories.Interfaces;

namespace MyLeasing.Common.Repositories;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(DataContext dataContext) : base(dataContext)
    {
    }
}