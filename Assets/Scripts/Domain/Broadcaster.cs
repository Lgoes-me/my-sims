using System.Collections.Generic;

namespace Domain
{
    public class Broadcaster
    {
        public List<Advertisement> Advertisements { get; }

        public Broadcaster(List<Advertisement> advertisements)
        {
            Advertisements = advertisements;

            foreach (var advertisement in Advertisements)
            {
                advertisement.RegisterBroadcaster(this);
            }
        }
        
        public virtual void OnInteractionStart(Character character)
        {
            
        }
        
        public virtual void OnInteractionFinish()
        {
            
        }
    }
}