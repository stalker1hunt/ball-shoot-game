using System;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class ShotController : MonoBehaviour
    {
        public float explosionRadius = 5f;
        
        public float speed = 10f;


        private Rigidbody rb;
        public float stoppingDistance = 0.1f;

        
        private Transform target;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Initiate(Transform shootDirection)
        {
            target = shootDirection;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger with " + other.gameObject.name);
            
            if (other.gameObject.CompareTag("Obstacle"))
            {
                InfectObstacles();
                Destroy(gameObject);
            }
        }

        void InfectObstacles()
        {
            Debug.Log("Infecting obstacles");
            
            float currentExplosionRadius = explosionRadius * transform.localScale.x;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentExplosionRadius);
        
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Obstacle"))
                {
                    hitCollider.gameObject.GetComponent<ObstacleController>().Infect();
                }
            }
        }
        
        public void SetExplosionRadius(float scale)
        {
            explosionRadius *= scale;
        }

        /*
        private void Update()
        {
            if(target == null) return;
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
              //  Destroy(gameObject);
            }
        }*/
        
        private void FixedUpdate()
        {
            if(target == null) return;

            Vector3 newPosition = Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);

            if (Vector3.Distance(rb.position, target.position) < stoppingDistance)
            {
              //  Destroy(gameObject);
            }
        }
    }
}