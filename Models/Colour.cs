using System.Drawing;
using Newtonsoft.Json;
using Omu.ValueInjecter;

namespace Ltht.TechTest.Models
{
    public class Colour
    {
        public Colour()
        {
        }

        public Colour(Entities.Colour colour)
        {
            this.InjectFrom(colour);
        }

        [JsonProperty("colourId")]
        public int ColourId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}