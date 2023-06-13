using System.Collections.Generic;

namespace RestWithNETUdemy.Hypermedia.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
