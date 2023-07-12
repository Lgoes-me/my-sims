using Data;
using Domain;
using Domain.Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class CharacterController : BroadcasterController, IMovableTransform, IInteractable
    {
        [field: SerializeField] private CharacterData CharacterData { get; set; }
        
        private Character Character { get; set; }
        private NavMeshAgent NavMeshAgent { get; set; }
        private CharacterMenuController CharacterMenuController { get; set; }
        private Movement Movement { get; set; }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Init(
            BroadcasterManager broadcasterManager,
            TimeManager timeManager,
            CharacterMenuController characterMenuController)
        {
            base.Init(broadcasterManager);
            
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
            if (Movement == null) return;
            
            NavMeshAgent.SetDestination(Movement.Destination.Position);
                
            if (Movement.CheckForDestinationReached(NavMeshAgent)) 
                Movement = null;
        }
        
        public void MoveTo(Movement movement)
        {
            Movement = movement;
        }

        [ContextMenu("Click")]
        private void OnMouseUp()
        {
            CharacterMenuController.Show(Character);
        }

        public void OnInteractionStart(Character character)
        {
            NavMeshAgent.isStopped = true;
            Character.Pause();
        }

        public void OnInteractionFinish()
        {
            NavMeshAgent.isStopped = false;
            Character.Resume();
        }
    }
}