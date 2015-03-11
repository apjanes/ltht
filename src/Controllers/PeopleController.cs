using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Models;
using Omu.ValueInjecter;

namespace Ltht.TechTest.Controllers
{
    public class PeopleController: ApiController
    {
        public void Put(int id, PersonDetail detail)
        {
            using (var entities = new TechTestEntities())
            {
                var colourIds = detail.Colours.Select(x => x.ColourId).ToArray();
                var entity = entities.People.Include("Colours").Single(x => x.PersonId == id);
                var colours = entities.Colours.Where(x => colourIds.Any(y => y == x.ColourId));
                entity.Colours.Clear();
                foreach (var colour in colours)
                {
                    entity.Colours.Add(colour);
                }
                entity.InjectFrom(detail);
                entities.SaveChanges();

            }
        }

        public PersonDetail Get(int id)
        {
            using (var entities = new TechTestEntities())
            {
                var entity = entities.People
                                     .Include("Colours")
                                     .Single(x => x.PersonId == id);
                return new PersonDetail(entity);
            }
        }

        public PersonSummary[] Get()
        {
            using (var entities = new TechTestEntities())
            {
                return entities.People
                               .Include("Colours")
                               .ToList()
                               .Select(person => new PersonSummary(person))
                               .ToArray();
            }
        }     
    }
}