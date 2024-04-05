using BallGame.Gameplay.PlayerBall;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class DoorController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private Transform _targetPosition;
        public Transform TargetPosition => _targetPosition; 
        
        [SerializeField]
        private Material _openMaterial;

        private PlayerBallController _playerBallController;
        
        public void Initialization()
        {
            _playerBallController = ServiceLocator.GetService<PlayerBallController>();
            _playerBallController.OnWin += OpenDoor;
            
            Application.quitting += ApplicationOnquitting;
        }

        private void ApplicationOnquitting()
        {
            _openMaterial.color = Color.white;
        }

        private void OpenDoor()
        {
            _openMaterial.color = Color.green;
        }
    }
}