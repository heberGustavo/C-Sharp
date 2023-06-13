using RestWithNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithNETUdemy.Hypermedia.Filters
{
    public class HyperMediaFiltersOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}