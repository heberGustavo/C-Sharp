using RestWithNETUdemy.Hypermedia;
using RestWithNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithNETUdemy.Data.VO
{
    public class PersonVO : ISupportsHypermedia
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[JsonIgnore]
        public string Address { get; set; }

        //[JsonPropertyName("sex")]
        public string Gender { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
