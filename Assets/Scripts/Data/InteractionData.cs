using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interface;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InteractionData", menuName = "ScriptableObjects/InteractionData")]
    public class InteractionData : ScriptableObject
    {
        [field: SerializeField] private List<ResolutionData> ResolutionDatas { get; set; }
        [field: SerializeField] private int Duration { get; set; }

        public Interaction ToDomain(
            string broadcasterName,
            ITransform transform, 
            Action<Character> onInteractionStart, 
            Action onInteractionFinish)
        {
            return new Interaction(
                name,
                broadcasterName,
                ResolutionDatas.Select(i => i.ToDomain()).ToList(),
                Duration,
                transform,
                onInteractionStart,
                onInteractionFinish);
        }
    }
}