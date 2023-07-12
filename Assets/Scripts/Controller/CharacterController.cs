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
        public Movement Movement { get; private set; }

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
            {
                NavMeshAgent.isStopped = true;
                Movement = null;
            }
        }
        
        public void MoveTo(Movement movement)
        {
            NavMeshAgent.isStopped = false;
            Movement = movement;
        }

        [ContextMenu("Click")]
        private void OnMouseUp()
        {
            CharacterMenuController.Show(Character);
        }
    }
}