using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interface;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AdvertisementData", menuName = "ScriptableObjects/AdvertisementData")]
    public class AdvertisementData : ScriptableObject
    {
        [field: SerializeField] private List<ResolutionData> ResolutionDatas { get; set; }
        [field: SerializeField] private int Duration { get; set; }

        public Advertisement ToDomain(ITransform transform)
        {
            return new Advertisement(
                name,
                ResolutionDatas.Select(i => i.ToDomain()).ToList(),
                transform,
                Duration);
        }
    }
}