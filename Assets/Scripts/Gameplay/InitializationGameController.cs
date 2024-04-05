using System.Collections.Generic;
using BallGame.Configs;
using BallGame.Gameplay.Obstacle;
using BallGame.Gameplay.PlayerBall;
using BallGame.Initialization;
using BallGame.UI;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class InitializationGameController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private PlayerBallSpawnController _playerBallSpawnController;
        [SerializeField]
        private ObstacleSpawnController _obstacleSpawnController;
        [SerializeField]
        private DoorController _doorController;
        [SerializeField]
        private PathRenderer _pathRenderer;
        [SerializeField]
        private ScreenUiController _screenUiController;
        
        private readonly List<IInitializationCommand> _initializationCommands = new();

        public void Initialization()
        {
            PlayerBallConfig playerBallConfig = ServiceLocator.GetService<ConfigService>()
                .GetConfig<PlayerBallConfig>(ConfigsConstants.PlayerBallConfigKey);
            
            playerBallConfig.SetupTarget(_doorController.TargetPosition);
            
            ServiceLocator.RegisterService(_screenUiController);

            AddInitializationGameCommand(new InitializationGameCommand<PlayerBallSpawnController>(_playerBallSpawnController));
            AddInitializationGameCommand(new InitializationGameCommand<DoorController>(_doorController));
            AddInitializationGameCommand(new InitializationGameCommand<ObstacleSpawnController>(_obstacleSpawnController));
            AddInitializationGameCommand(new InitializationGameCommand<PathRenderer>(_pathRenderer));
            AddInitializationGameCommand(new InitializationGameCommand<ScreenUiController>(_screenUiController));
            
            foreach (var command in _initializationCommands)
            {
                command.Execute();
            }
            
            var screen = _screenUiController.ShowScreenById(ScreenConstants.StartGameScreen);
            screen.Initialization();
        }
        
        private void AddInitializationGameCommand(IInitializationCommand command)
        {
            _initializationCommands.Add(command);
        }
    }
}