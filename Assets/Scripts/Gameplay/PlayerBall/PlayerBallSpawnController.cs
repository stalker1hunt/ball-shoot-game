using BallGame.Configs;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallSpawnController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private Transform _playerBallSpawnPoint;
        
        private PlayerBallConfig _playerBallConfig;

        public void Initialization()
        {
            _playerBallConfig = ServiceLocator.GetService<ConfigService>()
                .GetConfig<PlayerBallConfig>(ConfigsConstants.PlayerBallConfigKey);

            SpawnPlayerBall();
        }
        
        private void SpawnPlayerBall()
        {
            Instantiate(_playerBallConfig.PlayerBallPrefab, _playerBallSpawnPoint.position, Quaternion.identity);
        }
    }
}