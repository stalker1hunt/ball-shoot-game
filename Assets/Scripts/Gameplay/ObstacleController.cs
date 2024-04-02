using UnityEngine;

namespace BallGame.Gameplay
{
    public class ObstacleController : MonoBehaviour
    {
        public GameObject explosionEffect;

        public void Infect()
        {
            gameObject.AddComponent<Infection>().StartInfection();
        }
    }
}