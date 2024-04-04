using System.Collections.Generic;
using BallGame.Configs;
using BallGame.Gameplay;
using BallGame.Instance;
using BallGame.StateMachine.States;
using UnityEngine;

namespace BallGame.Initialization
{
    public class InitializationController : MonoBehaviour
    {
        [SerializeField]
        private InitializationGameController _initializationGameController;

        private List<IInitializationCommand> _initializationCommands = new List<IInitializationCommand>();

        private void Awake()
        {
            var configService = new ConfigService();
            
            var infectionConfig = configService.GetConfig<InfectionConfig>(ConfigsConstants.InfectionConfigKey);
            var particleSystemFactory = new ObjectFactory<ParticleSystem>(infectionConfig.InfectionEffect);
            
            var stateMachine = new StateMachine.StateMachine();
            stateMachine.ChangeState(new StartState());
            
            //var ballFactory = new ObjectFactory<Ball>(infectionConfig.BallPrefab);
            
            AddInitializationCommand(new RegisterServiceCommand<ObjectFactory<ParticleSystem>>(particleSystemFactory));
            AddInitializationCommand(new RegisterServiceCommand<ConfigService>(configService));
            AddInitializationCommand(new RegisterServiceCommand<StateMachine.StateMachine>(stateMachine));
            
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