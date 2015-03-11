using System.Data.Entity;
using System.Linq;
using Ltht.TechTest.Entities;

namespace Ltht.TechTest.Repositories
{
    public class ColourRepository : RepositoryBase<Colour>, IColourRepository
    {
        public ColourRepository(IDbContextProvider provider) : base(provider)
        {
        }
    }
}