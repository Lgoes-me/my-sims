using System.Collections.Generic;
using System.Linq;
using Domain;
using UnityEngine;

namespace Manager
{
    public class BroadcasterManager
    {
        private List<Broadcaster> Broadcasters { get; }

        public BroadcasterManager()
        {
            Broadcasters = new List<Broadcaster>();
        }

        public void Subscribe(Broadcaster broadcaster)
        {
            if (Broadcasters.Contains(broadcaster))
                return;

            Broadcasters.Add(broadcaster);
        }

        public Advertisement FindAdvertisement(Character character)
        {
            var advertisements = Broadcasters
                .SelectMany(b => b.Advertisements)
                .OrderBy(i =>
                {
                    var value = 0.0f;

                    foreach (var resolution in  i.Resolutions)
                    {
                        if (character.Motives.TryGetValue(resolution.Need, out var motive))
                        {
                            value += resolution.Rate * motive.Urgency;
                            value += Vector3.Distance(i.Transform.Position, character.Transform.Position) * 0.1f;
                        }
                    }
                    
                    return value;
                    
                })
                .Except(character.Advertisements)
                .ToList();

            var upperLimit = Mathf.Min(advertisements.Count, 4);
            
            return advertisements[Random.Range(0, upperLimit)];
        }
        
        public Advertisement FindBestResponse(List<Advertisement> responses, Character character)
        {
            var advertisements = responses
                .OrderBy(i =>
                {
                    var value = 0.0f;

                    foreach (var resolution in  i.Resolutions)
                    {
                        if (character.Motives.TryGetValue(resolution.Need, out var motive))
                        {
                            value += resolution.Rate * motive.Urgency;
                            value += Vector3.Distance(i.Transform.Position, character.Transform.Position) * 0.1f;
                        }
                    }
                    
                    return value;
                    
                })
                .ToList();

            var upperLimit = Mathf.Min(advertisements.Count, 4);
            
            return advertisements[Random.Range(0, upperLimit)];
        }
    }
}