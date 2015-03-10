using System.Linq;
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
            get
            {
                var name = Name.Replace(" ", string.Empty).ToLower();
                var reversed = new string(name.Reverse().ToArray());
                return name == reversed;
            }
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