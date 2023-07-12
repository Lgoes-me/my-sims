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

        public Advertisement(
            string name,
            List<Resolution> resolutions,
            ITransform transform,
            int duration)
        {
            Name = name;
            Resolutions = resolutions;
            Transform = transform;
            Duration = duration;
        }

        public Interaction StartInteraction(Action callback)
        {
            return new Interaction(this, Duration, callback);
        }
    }
}