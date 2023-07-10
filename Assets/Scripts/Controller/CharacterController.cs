using Data;
using Domain;
using Domain.Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class CharacterController : BroadcasterController, IMovableTransform
    {
        [field: SerializeField] private CharacterData CharacterData { get; set; }
        
        private Character Character { get; set; }
        private NavMeshAgent NavMeshAgent { get; set; }
        private CharacterMenuController CharacterMenuController { get; set; }
        private MovementData MovementData { get; set; }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Init(
            BroadcasterManager broadcasterManager,
            TimeManager timeManager,
            CharacterMenuController characterMenuController)
        {
            base.Init(broadcasterManager, timeManager);
            
            Character = CharacterData.ToDomain(
                gameObject.name, 
                Broadcaster,
                broadcasterManager, 
                this);
            
            timeManager.Subscribe(Character);

            Character.Init();
            CharacterMenuController = characterMenuController;
        }

        private void Update()
        {
            if (MovementData == null) return;
            
            NavMeshAgent.SetDestination(MovementData.Destination.Position);
                
            if (MovementData.CheckForDestinationReached(NavMeshAgent)) 
                MovementData = null;
        }
        
        public void MoveTo(MovementData movementData)
        {
            MovementData = movementData;
        }

        [ContextMenu("Click")]
        private void OnMouseUp()
        {
            CharacterMenuController.Show(Character);
        }

        protected override void OnInteractionStart(Character character)
        {
            base.OnInteractionStart(character);
            NavMeshAgent.isStopped = true;
            
            Character.Pause();
        }

        protected override void OnInteractionFinish()
        {
            base.OnInteractionFinish();
            NavMeshAgent.isStopped = false;
            
            Character.Resume();
        }
    }
}