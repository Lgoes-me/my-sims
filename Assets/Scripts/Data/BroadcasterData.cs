using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interface;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BroadcasterData
    {
        [field: SerializeField] protected List<AdvertisementData> InteractionsData { get; set; }

        public Broadcaster ToDomain(ITransform transform)
        {
            return new Broadcaster(InteractionsData.Select(i => i.ToDomain(transform)).ToList());
        }
    }
}