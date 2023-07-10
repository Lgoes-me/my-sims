using Manager;
using UnityEngine;

namespace Controller
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private BroadcasterController[] Broadcasters { get; set; }
        [field: SerializeField] private CharacterController[] Characters { get; set; }
        [field: SerializeField] private CharacterMenuController CharacterMenuController { get; set; }
        
        private void Start()
        {
            var gameManager = new GameManager();
            
            foreach (var broadcaster in Broadcasters)
            {
                broadcaster.Init(gameManager.BroadcasterManager, gameManager.TimeManager);
            }
            
            foreach (var character in Characters)
            {
                character.Init(gameManager.BroadcasterManager, gameManager.TimeManager, CharacterMenuController);
            }

            StartCoroutine(gameManager.TimeManager.Update());
        }
    }
}