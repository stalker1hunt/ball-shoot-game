using System.Collections.Generic;
using BallGame.Configs;
using BallGame.Instance;
using UnityEngine;

namespace BallGame.Initialization
{
    public class InitializationController : MonoBehaviour
    {
        private List<IInitializationCommand> _initializationCommands = new List<IInitializationCommand>();

        private void Awake()
        {
            var configService = new ConfigService();
            var infectionConfig = configService.GetConfig<InfectionConfig>(ConfigsConstants.InfectionConfigKey);
            var particleSystemFactory = new ObjectFactory<ParticleSystem>(infectionConfig.InfectionEffect);
            
            AddInitializationCommand(new RegisterServiceCommand<ObjectFactory<ParticleSystem>>(particleSystemFactory));
            AddInitializationCommand(new RegisterServiceCommand<ConfigService>(new ConfigService()));
            
            foreach (var command in _initializationCommands)
            {
                command.Execute();
            }
        }

        public void AddInitializationCommand(IInitializationCommand command)
        {
            _initializationCommands.Add(command);
        }
    }
}