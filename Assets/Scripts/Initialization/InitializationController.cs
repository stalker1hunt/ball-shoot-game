﻿using System.Collections.Generic;
using BallGame.Configs;
using BallGame.Gameplay;
using BallGame.Gameplay.Obstacle;
using BallGame.Instance;
using BallGame.StateMachine.States;
using UnityEngine;

namespace BallGame.Initialization
{
    public class InitializationController : MonoBehaviour
    {
        [SerializeField]
        private InitializationGameController _initializationGameController;

        private List<IInitializationCommand> _initializationCommands = new();

        private void Awake()
        {
            var configService = new ConfigService();
            
            var infectionConfig = configService.GetConfig<InfectionConfig>(ConfigsConstants.InfectionConfigKey);
            var playerBallConfig = configService.GetConfig<PlayerBallConfig>(ConfigsConstants.PlayerBallConfigKey);
            var obstacleConfig = configService.GetConfig<ObstacleConfig>(ConfigsConstants.ObstacleConfigKey);
            
            var particleSystemFactory = new ObjectFactory<ParticleSystem>(infectionConfig.InfectionEffect);
            var ballShotFactory = new ObjectFactory<BallShotController>(playerBallConfig.BallShotPrefab);
            var obstacleFactory = new ObjectFactory<ObstacleController>(obstacleConfig.ObstaclePrefab);
            
            var stateMachine = new StateMachine.StateMachine();
            stateMachine.ChangeState(new StartState());
            
            AddInitializationCommand(new RegisterServiceCommand<ConfigService>(configService));
            AddInitializationCommand(new RegisterServiceCommand<StateMachine.StateMachine>(stateMachine));
            AddInitializationCommand(new RegisterServiceCommand<GameState>(new GameState()));
            
            AddInitializationCommand(new RegisterServiceCommand<ObjectFactory<ParticleSystem>>(particleSystemFactory));
            AddInitializationCommand(new RegisterServiceCommand<ObjectFactory<BallShotController>>(ballShotFactory));
            AddInitializationCommand(new RegisterServiceCommand<ObjectFactory<ObstacleController>>(obstacleFactory));

            foreach (var command in _initializationCommands)
            {
                command.Execute();
            }
            
            _initializationGameController.Initialization();
        }

        private void AddInitializationCommand(IInitializationCommand command)
        {
            _initializationCommands.Add(command);
        }
    }
}