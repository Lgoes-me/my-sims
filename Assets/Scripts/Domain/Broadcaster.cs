using System.Collections.Generic;

namespace Domain
{
    public class Broadcaster
    {
        public List<Advertisement> Advertisements { get; }

        public Broadcaster(List<Advertisement> advertisements)
        {
            Advertisements = advertisements;
        }
    }
}