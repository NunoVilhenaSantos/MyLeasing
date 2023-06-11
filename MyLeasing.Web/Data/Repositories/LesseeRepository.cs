using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.Repositories.Interfaces;

namespace MyLeasing.Web.Data.Repositories;

public class LesseeRepository : GenericRepository<Lessee>, ILesseeRepository
{
    public LesseeRepository(DataContext dataContext) : base(dataContext)
    {
    }
}