using System;
using System.Collections.Generic;
using Domain.Interface;

namespace Domain
{
    public class Advertisement
    {
        public string Name { get; }
        public List<Resolution> Resolutions { get; }
        public ITransform Transform { get; }
        private int Duration { get; }
        public List<Advertisement> Responses { get; }
        
        public Broadcaster Broadcaster { get; private set; }

        public Advertisement(
            string name,
            List<Resolution> resolutions,
            ITransform transform,
            int duration,
            List<Advertisement> responses)
        {
            Name = name;
            Resolutions = resolutions;
            Transform = transform;
            Duration = duration;
            Responses = responses;
        }

        public Interaction StartInteraction(Action callback)
        {
            return new Interaction(this, Duration, callback);
        }

        public void RegisterBroadcaster(Broadcaster broadcaster)
        {
            Broadcaster = broadcaster;
        }
    }
}