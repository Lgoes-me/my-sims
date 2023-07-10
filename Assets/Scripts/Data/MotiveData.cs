using Domain;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MotiveData", menuName = "ScriptableObjects/MotiveData")]
    public class MotiveData : ScriptableObject
    {
        [field: SerializeField] private NeedData Need { get; set; }
        [field: SerializeField] private AnimationCurve Curve { get; set; }
        [field: SerializeField] private float Rate { get; set; }
        [field: SerializeField, Range(0, 100)] private int DefaultValue { get; set; }

        public Motive ToDomain()
        {
            return new Motive(
                Need.ToDomain(),
                Curve,
                Rate,
                Random.Range(0, DefaultValue));
        }
    }
}