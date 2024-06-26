﻿using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}