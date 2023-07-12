using Controller;
using Manager;
using UnityEngine;
using CharacterController = Controller.CharacterController;

namespace Scenes
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
                broadcaster.Init(gameManager.BroadcasterManager);
            }
            
            foreach (var character in Characters)
            {
                character.Init(gameManager.BroadcasterManager, gameManager.TimeManager, CharacterMenuController);
            }

            StartCoroutine(gameManager.TimeManager.Update());
        }
    }
}