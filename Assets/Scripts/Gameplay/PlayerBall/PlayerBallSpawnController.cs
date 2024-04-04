using BallGame.Configs;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallSpawnController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private Transform _playerBallSpawnPoint;
        
        public void Initialization()
        {
           var playerBallConfig = ServiceLocator.GetService<ConfigService>()
                .GetConfig<PlayerBallConfig>(ConfigsConstants.PlayerBallConfigKey);

            Instantiate(playerBallConfig.PlayerBallPrefab, _playerBallSpawnPoint.position, Quaternion.identity).Setup(playerBallConfig);
        }
    }
}