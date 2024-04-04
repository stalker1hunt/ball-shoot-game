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
        private float maxShotScale = 0.3f; 

        [SerializeField]
        private float shotChargeRate = 0.5f;
     
        [SerializeField]
        private float minPlayerScale = 0.5f;
       
        public float MaxShotScale { get; }
        public float ShotChargeRate { get; }
        public float MinPlayerScale { get; }
        

        public Transform TargetPosition { get; private set; }
        public void SetupTarget(Transform target) => TargetPosition = target;
    }
}