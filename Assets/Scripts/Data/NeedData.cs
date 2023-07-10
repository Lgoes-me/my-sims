using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NeedData", menuName = "ScriptableObjects/NeedData")]
    public class NeedData : ScriptableObject
    {
        public string ToDomain()
        {
            return name;
        }
    }
}