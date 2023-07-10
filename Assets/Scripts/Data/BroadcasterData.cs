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
        [field: SerializeField] private List<InteractionData> InteractionsData { get; set; }

        public Broadcaster ToDomain(
            string name,
            ITransform transform,
            Action<Character> onInteractionStart,
            Action onInteractionFinish)
        {
            return new Broadcaster(
                InteractionsData.Select(i => i.ToDomain(
                    name,
                    transform,
                    onInteractionStart,
                    onInteractionFinish)).ToList());
        }
    }
}