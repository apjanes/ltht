using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Omu.ValueInjecter;

namespace Ltht.TechTest.Models
{
    public class PersonBase
    {
        public PersonBase()
        {
        }

        public PersonBase(Entities.Person person)
        {
            this.InjectFrom(person);
            Colours = person.Colours.Select(x => new Colour(x)).ToArray();
        }

        [JsonProperty("href")]
        public string Href
        {
            get { return "/api/people/" + PersonId; }
        }

        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("name")]
        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("isAuthorised")]
        public bool IsAuthorised { get; set; }

        [JsonProperty("isEnabled")]
        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        public virtual Colour[] Colours { get; set; }
    }
}