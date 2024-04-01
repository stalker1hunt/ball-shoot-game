using UnityEngine;

namespace BallGame.Gameplay
{
    public class ObstacleController : MonoBehaviour
    {
        public GameObject explosionEffect;

        public void Infect()
        {
            if (explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision with " + collision.gameObject.name);
        }
    }
}