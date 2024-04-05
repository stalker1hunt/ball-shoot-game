using BallGame.Gameplay;
using BallGame.Gameplay.PlayerBall;
using UnityEngine;

namespace BallGame.Configs
{
    [CreateAssetMenu(fileName = "PlayerBall", menuName = "Create PlayerBall", order = 0)]
    public class PlayerBallConfig : ScriptableObject
    {
        [SerializeField]
        private PlayerBallController _playerBallPrefab;
        public PlayerBallController PlayerBallPrefab => _playerBallPrefab;
        
        [SerializeField]
        private BallShotController _ballShotPrefab;
        public BallShotController BallShotPrefab => _ballShotPrefab;
        
        [SerializeField]
        private float _maxShotScale = 0.3f; 
        public float MaxShotScale => _maxShotScale;
        
        [SerializeField]
        private float _shotChargeRate = 0.5f;
        public float ShotChargeRate => _shotChargeRate;

        [SerializeField]
        private float _minPlayerScale = 0.5f;
        public float MinPlayerScale => _minPlayerScale;

        public Transform TargetPosition { get; private set; }
        public void SetupTarget(Transform target) => TargetPosition = target;
    }
}