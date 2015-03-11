using System.Collections;
using System.Collections.Generic;
using System.Web;
using Ltht.TechTest.Entities;

namespace Ltht.TechTest.Repositories
{
    public class DbContextProvider : IDbContextProvider
    {
        private const string ItemsKey = "DbContext";
        private IDictionary _localItems;

        public TechTestEntities Get()
        {
            if (!Items.Contains(ItemsKey))
            {
                Items.Add(ItemsKey, new TechTestEntities());
            }

            return (TechTestEntities) Items[ItemsKey];
        }

        private IDictionary Items
        {
            get
            {
                if (HttpContext.Current != null) return HttpContext.Current.Items;
                return _localItems ?? (_localItems = new Dictionary<string, TechTestEntities>());
            }
        }
    }
}