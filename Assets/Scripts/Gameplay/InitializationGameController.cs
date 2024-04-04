using System.Collections.Generic;
using BallGame.Configs;
using BallGame.Gameplay.PlayerBall;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class InitializationGameController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private PlayerBallSpawnController _playerBallSpawnController;
        [SerializeField]
        private DoorController _doorController;
        
        private readonly List<IInitializationCommand> _initializationCommands = new();

        public void Initialization()
        {
            PlayerBallConfig playerBallConfig = ServiceLocator.GetService<ConfigService>()
                .GetConfig<PlayerBallConfig>(ConfigsConstants.PlayerBallConfigKey);
            
            playerBallConfig.SetupTarget(_doorController.TargetPosition);
            
            AddInitializationGameCommand(new InitializationGameCommand<PlayerBallSpawnController>(_playerBallSpawnController));
            AddInitializationGameCommand(new InitializationGameCommand<DoorController>(_doorController));
            
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