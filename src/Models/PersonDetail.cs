using System.Linq;
using System.Runtime.Serialization;
using Ltht.TechTest.Entities;
using Newtonsoft.Json;
using Omu.ValueInjecter;

namespace Ltht.TechTest.Models
{
    public class PersonDetail : PersonBase
    {
        public PersonDetail()
        {
        }

        public PersonDetail(Entities.Person person) : base(person)
        {
        }

        [JsonProperty("colours")]
        [DataMember(Name="colours")]
        public override Colour[] Colours { get; set; }
    }
}