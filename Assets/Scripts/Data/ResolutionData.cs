﻿using UnityEngine;
using Resolution = Domain.Resolution;

namespace Data
{
    [System.Serializable]
    public class ResolutionData
    {
        [field: SerializeField]
        private NeedData Need { get; set; }
    
        [field: SerializeField]
        private float Rate { get; set; }
    
        public Resolution ToDomain()
        {
            return new Resolution(
                Need.ToDomain(),
                Rate);
        }
    }
}