using BallGame.Gameplay.Obstacle;
using UnityEngine;

namespace BallGame.Configs
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "Create Obstacle", order = 0)]
    public class ObstacleConfig : ScriptableObject
    {
        [SerializeField]
        private ObstacleController _obstaclePrefab;
        public ObstacleController ObstaclePrefab => _obstaclePrefab;
    }
}