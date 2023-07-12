using UnityEngine;

namespace Domain
{
    [System.Serializable]
    public class Motive
    {
        public string Need { get; private set; }
        public float Urgency { get; private set; }

        private AnimationCurve Curve { get; set; }
        private float DefaultRate { get; set; }
        private float Rate { get; set; }
        private float CurrentValue { get; set; }

        public Motive(
            string need,
            AnimationCurve curve,
            float rate,
            int defaultValue)
        {
            Need = need;
            Urgency = 0;

            Curve = curve;
            
            DefaultRate = rate;
            Rate = rate;
            
            CurrentValue = defaultValue;
        }

        public void OnTick()
        {
            CurrentValue = Mathf.Clamp(CurrentValue + Rate, 0, 100);
            Urgency = Curve.Evaluate(Mathf.InverseLerp(0, 100, CurrentValue));
        }

        public void InitResolution(Resolution resolution)
        {
            Rate = resolution.Rate;
        }

        public void FinishResolution()
        {
            Rate = DefaultRate;
        }
    }
}