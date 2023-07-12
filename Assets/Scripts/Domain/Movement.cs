using System;
using Domain.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Domain
{
    public class Movement
    {
        public ITransform Destination { get; }

        private float StartTime { get; }
        private Action OnDestinationReached { get; }
        
        private bool IsStopped { get; set; }

        public Movement(ITransform destination, Action onDestinationReached)
        {
            StartTime = Time.time;
            Destination = destination;
            OnDestinationReached = onDestinationReached;
        }

        public bool CheckForDestinationReached(NavMeshAgent navMeshAgent)
        {
            if (IsStopped)
                return true;
            
            var hasReachedDestination =
                Time.time - StartTime > 1f &&
                navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                navMeshAgent.remainingDistance <= 0.5f;

            if (!hasReachedDestination)
                return false;

            OnDestinationReached();
            return true;
        }

        public void Stop()
        {
            IsStopped = true;
        }
    }
}