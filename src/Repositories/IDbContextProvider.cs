using Ltht.TechTest.Entities;

namespace Ltht.TechTest.Repositories
{
    public interface IDbContextProvider
    {
        TechTestEntities Get();
    }
}