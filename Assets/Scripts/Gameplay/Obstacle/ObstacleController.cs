using UnityEngine;

namespace BallGame.Gameplay.Obstacle
{
    public class ObstacleController : MonoBehaviour
    {
        public Infection Infection { get; private set; }
      
        public void AddInfection()
        {
            Infection = gameObject.AddComponent<Infection>();
        }
        
        public void StartInfection()
        {
            Infection.StartInfection();
        }
    }
}