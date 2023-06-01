using MyLeasing.Common.DataContexts;
using MyLeasing.Common.Entities;

namespace MyLeasing.Common.Repositories;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(DataContext dataContext) : base(dataContext)
    {
    }
}