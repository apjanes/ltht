using System;
using System.Linq;

namespace Ltht.TechTest.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Query();
        void Save();
    }
}