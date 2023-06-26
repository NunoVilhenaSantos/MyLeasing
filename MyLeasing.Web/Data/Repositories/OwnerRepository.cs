using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.Repositories.Interfaces;

namespace MyLeasing.Web.Data.Repositories;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(DataContextMSSQL dataContextMssql) : base(
        dataContextMssql)
    {
    }
}