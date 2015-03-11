using System.Linq;
using Ltht.TechTest.Extensions;
using Newtonsoft.Json;

namespace Ltht.TechTest.Models
{
    public class PersonSummary : PersonBase
    {
        public PersonSummary()
        {
        }

        public PersonSummary(Entities.Person person) : base(person)
        {
        }

        [JsonProperty("isPalindrome")]
        public bool IsPalindrome
        {
            get { return Name.IsPalindrome(); }
        }

        [JsonIgnore]
        public override Colour[] Colours { get; set; }

        [JsonProperty("colours")]
        public string ColourNames
        {
            get
            {
                return string.Join(", ", Colours.Select(x => x.Name));
            }
        }
    }
}