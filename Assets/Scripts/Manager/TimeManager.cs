using System.Collections;
using System.Collections.Generic;
using Domain.Interface;
using UnityEngine;

namespace Manager
{
    public class TimeManager
    {
        private List<ITimeManagerListener> TimeManagerListeners { get; }
        private bool Stopped { get; set; }

        public TimeManager()
        {
            TimeManagerListeners = new List<ITimeManagerListener>();
        }


        public void Subscribe(ITimeManagerListener listener)
        {
            if (TimeManagerListeners.Contains(listener))
                return;

            TimeManagerListeners.Add(listener);
        }
        
        public IEnumerator Update()
        {
            while (!Stopped)
            {
                yield return new WaitForSeconds(1f);

                foreach (var listener in TimeManagerListeners)
                {
                    listener.Tick();
                }
            }
        }
    }
}