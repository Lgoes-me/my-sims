using System.Collections.Generic;
using Domain.Interface;

namespace Domain
{
    public class Broadcaster : ITimeManagerListener
    {
        public List<Interaction> Interactions { get; private set; }

        public Broadcaster(List<Interaction> interactions)
        {
            Interactions = interactions;
        }

        public void Tick()
        {
            foreach (var interaction in Interactions)
            {
                interaction.OnTick();
            }
        }
    }
}