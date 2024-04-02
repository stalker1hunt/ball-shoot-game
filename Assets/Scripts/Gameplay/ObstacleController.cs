using UnityEngine;

namespace BallGame.Gameplay
{
    public class ObstacleController : MonoBehaviour
    {
        public void Infect()
        {
            gameObject.AddComponent<Infection>().StartInfection();
        }
    }
}