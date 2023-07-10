using System;
using Domain.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Domain
{
    public class MovementData
    {
        public ITransform Destination { get; }

        private float StartTime { get; }
        private Action OnDestinationReached { get; }

        public MovementData(ITransform destination, Action onDestinationReached)
        {
            StartTime = Time.time;
            Destination = destination;
            OnDestinationReached = onDestinationReached;
        }

        public bool CheckForDestinationReached(NavMeshAgent navMeshAgent)
        {
            var hasReachedDestination =
                Time.time - StartTime > 1f &&
                navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                navMeshAgent.remainingDistance <= 0.5f;

            if (!hasReachedDestination)
                return false;

            OnDestinationReached();
            return true;
        }
    }
}