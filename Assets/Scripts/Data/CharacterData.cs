using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interface;
using Manager;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class CharacterData
    {
        [field: SerializeField] private List<MotiveData> MotivesData { get; set; }
        [field: SerializeField] private AdvertisementData PassiveAdvertisement { get; set; }

        public Character ToDomain(
            string name,
            Broadcaster broadcaster,
            BroadcasterManager broadcasterManager, 
            IMovableTransform transform)
        {
            return new Character(
                name,
                MotivesData.Select(m => m.ToDomain()).ToList(),
                broadcaster.Advertisements,       
                PassiveAdvertisement.ToDomain(transform),
                broadcasterManager,
                transform);
        }
    }
}