using Data;
using Domain;
using Domain.Interface;
using Manager;
using UnityEngine;

namespace Controller
{
    public class BroadcasterController : MonoBehaviour, ITransform
    {
        public Vector3 Position => gameObject.transform.position;
        
        [field: SerializeField] private BroadcasterData BroadcasterData { get; set; }
        protected Broadcaster Broadcaster { get; set; }
        
        public void Init(BroadcasterManager broadcasterManager, TimeManager timeManager)
        {
            Broadcaster = BroadcasterData.ToDomain(
                name, 
                this,
                OnInteractionStart, 
                OnInteractionFinish);
            
            broadcasterManager.Subscribe(Broadcaster);
            timeManager.Subscribe(Broadcaster);
        }

        protected virtual void OnInteractionStart(Character character)
        {
            
        }
        
        protected virtual void OnInteractionFinish()
        {
            
        }
    }
}