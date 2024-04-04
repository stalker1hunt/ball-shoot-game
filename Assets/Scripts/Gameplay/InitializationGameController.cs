using System.Collections.Generic;
using BallGame.Gameplay.PlayerBall;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class InitializationGameController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private PlayerBallSpawnController _playerBallSpawnController;

        private readonly List<IInitializationCommand> _initializationCommands = new List<IInitializationCommand>();

        public void Initialization()
        {
            AddInitializationGameCommand(new InitializationGameCommand<PlayerBallSpawnController>(_playerBallSpawnController));
            
            foreach (var command in _initializationCommands)
            {
                command.Execute();
            }
        }
        
        private void AddInitializationGameCommand(IInitializationCommand command)
        {
            _initializationCommands.Add(command);
        }
    }
}