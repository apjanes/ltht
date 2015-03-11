using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Models;
using Ltht.TechTest.Repositories;
using Omu.ValueInjecter;

namespace Ltht.TechTest.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IPersonRepository _personRepo;
        private readonly IColourRepository _colourRepo;

        public PeopleController(IColourRepository colourRepo, IPersonRepository personRepo)
        {
            _personRepo = personRepo;
            _colourRepo = colourRepo;
        }

        public void Put(int id, PersonDetail detail)
        {
            var colourIds = detail.Colours.Select(x => x.ColourId).ToArray();
            var entity = _personRepo.Query().Single(x => x.PersonId == id);
            var colours = _colourRepo.Query()
                                     .Where(x => colourIds.Any(y => y == x.ColourId))
                                     .ToList();
            entity.Colours.Clear();
            colours.ForEach(x => entity.Colours.Add(x));
            entity.InjectFrom(detail);
            _personRepo.Save();
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
            return _personRepo.Query()
                              .ToList()
                              .Select(person => new PersonSummary(person))
                              .ToArray();
        }
    }
}