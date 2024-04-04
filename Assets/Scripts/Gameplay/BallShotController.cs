using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class BallShotController : MonoBehaviour
    {
        public float explosionRadius = 20f;
        public float delayBetweenInfections = 0.1f;
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
                gameObject.SetActive(false);
            }
        }

        void InfectObstacles()
        {
            Debug.Log("Infecting obstacles");
            StartExplosionSequence();
        }
        
        private void StartExplosionSequence()
        {
            CoroutineManager.Instance.StartCoroutine("infection1", InfectObstaclesSequentially());
        }

        private IEnumerator InfectObstaclesSequentially()
        {
            float currentExplosionRadius = explosionRadius * transform.localScale.x;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentExplosionRadius);

            List<ObstacleController> obstaclesToInfect = new List<ObstacleController>();

            foreach (var hitCollider in hitColliders)
            {
                ObstacleController obstacleController = hitCollider.GetComponent<ObstacleController>();
                if (obstacleController != null)
                {
                    obstaclesToInfect.Add(obstacleController);
                }
            }
            
            Debug.Log(hitColliders.Length + " obstacles to infect");

            for (var index = 0; index < obstaclesToInfect.Count; index++)
            {
                var obstacle = obstaclesToInfect[index];
                try
                {
                    obstacle.Infect();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

                yield return new WaitForSeconds(delayBetweenInfections);
            }
        }
        
        public void SetExplosionRadius(float scale)
        {
            explosionRadius *= scale;
        }
        
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