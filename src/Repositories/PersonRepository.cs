using System.Data.Entity;
using System.Linq;
using Ltht.TechTest.Entities;

namespace Ltht.TechTest.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(IDbContextProvider provider) : base(provider)
        {
        }

        public override IQueryable<Person> Query()
        {
            return base.Query().Include("Colours");
        }
    }
}