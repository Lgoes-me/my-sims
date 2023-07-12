using System;
using UnityEngine;

namespace Domain
{
    public class Interaction
    {
        public Advertisement Advertisement { get; }
        private int MaxDuration { get; set; }
        private int CurrentDuration { get; set; }
        private bool IsInteracting { get; set; }
        private Action Callback { get; set; }

        public Interaction(Advertisement advertisement, int duration, Action callback)
        {
            Advertisement = advertisement;
            MaxDuration = duration;
            CurrentDuration = duration;
            IsInteracting = true;
            
            Callback = () =>
            {
                IsInteracting = false;
                Callback = null;

                callback();
            };
        }

        public void OnTick()
        {
            if(!IsInteracting)
                return;

            CurrentDuration = Mathf.Clamp(CurrentDuration - 1, 0, MaxDuration);

            if(CurrentDuration != 0)
                return;

            Callback?.Invoke();
        }

        public void ForceFinish()
        {            
            CurrentDuration = 0;
            Callback?.Invoke();
        }
    }
}