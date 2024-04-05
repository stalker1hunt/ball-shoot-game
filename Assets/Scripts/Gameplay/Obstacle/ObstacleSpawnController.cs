using BallGame.Initialization;
using BallGame.Instance;
using UnityEngine;

namespace BallGame.Gameplay.Obstacle
{
    public class ObstacleSpawnController : MonoBehaviour, IInitialization
    {
        private ObjectFactory<ObstacleController> _obstacleFactory;
 
        [SerializeField] 
        private float arenaSize;
        [SerializeField] 
        private int obstaclesCount;
        [SerializeField] 
        private float minDistance;
        
        [SerializeField]
        private Transform _obstacleParent;
        
        public void Initialization()
        {
            _obstacleFactory = ServiceLocator.GetService<ObjectFactory<ObstacleController>>();
            SpawnObstacles();
        }

        private void SpawnObstacles()
        {
            int attempts = 0;
            int maxAttempts = obstaclesCount * 10;

            for (int i = 0; i < obstaclesCount && attempts < maxAttempts; i++)
            {
                Vector3 position = GetRandomPosition();
                if (IsPositionValid(position))
                {
                    var obstacle = _obstacleFactory.CreateObject();
                    obstacle.transform.SetParent(_obstacleParent);
                    obstacle.transform.position = position;
                }
                else
                {
                    i--;
                    attempts++;
                }
            }
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-arenaSize / 2, arenaSize / 2),
                1f,
                Random.Range(-arenaSize / 2, arenaSize / 2)
            );
        }

        private bool IsPositionValid(Vector3 position)
        {
            foreach (var existingObstacle in FindObjectsOfType<ObstacleController>())
            {
                if (Vector3.Distance(position, existingObstacle.transform.position) < minDistance)
                    return false;
            }
            return true;
        }
    }
}