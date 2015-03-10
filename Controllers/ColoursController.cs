using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Models;
using Colour = Ltht.TechTest.Models.Colour;

namespace Ltht.TechTest.Controllers
{
    public class ColoursController: ApiController
    {
        public Colour[] Get()
        {
            using (var entities = new TechTestEntities())
            {
                return entities.Colours
                               .ToList()
                               .Select(x => new Colour(x))
                               .ToArray();
            }
        }     
    }
}