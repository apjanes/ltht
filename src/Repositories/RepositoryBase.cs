using System.Linq;
using Ltht.TechTest.Entities;

namespace Ltht.TechTest.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly TechTestEntities _dbContext;

        public RepositoryBase(IDbContextProvider provider)
        {
            _dbContext = provider.Get();
        }

        public virtual IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}