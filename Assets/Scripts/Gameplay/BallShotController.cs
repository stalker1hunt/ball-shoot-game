using System;
using System.Collections.Generic;
using BallGame.Gameplay.Obstacle;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class BallShotController : MonoBehaviour
    {
        public event Action<List<ObstacleController>> OnShotHitObstacle;
        
        [SerializeField]
        private Rigidbody _rigidbody; 
        
        public float explosionRadius = 20f;
        public float stoppingDistance = 0.1f;

        private Transform target;
        
        private Vector3 startPosition;
        private float trajectoryHeight = 0.5f;
        private float distanceToTarget;
        private float speed = 7f;
        private bool isMoving = false;
        private float time = 0f;

        private void FixedUpdate() 
        {
            if (isMoving)
            {
                time += Time.fixedDeltaTime * speed / distanceToTarget;
                if (time > 1f) {
                    isMoving = false;
                    time = 1f;
                }

                Vector3 horizontalPosition = Vector3.Lerp(startPosition, target.position, time);
                float height = Mathf.Sin(Mathf.Clamp01(time) * Mathf.PI) * trajectoryHeight;
                Vector3 combinedPosition = new Vector3(horizontalPosition.x, startPosition.y + height, horizontalPosition.z);
        
                _rigidbody.MovePosition(combinedPosition);
            }
        }
        
        public void Initiate(Transform shootDirection)
        {
            target = shootDirection;
            InitiateMove();
        }

        private void InitiateMove()
        {
            isMoving = true;
            startPosition = transform.position;
            distanceToTarget = Vector3.Distance(startPosition, target.position);
            time = 0f;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                var currentExplosionRadius = explosionRadius * transform.localScale.x;
                var hitColliders = Physics.OverlapSphere(transform.position, currentExplosionRadius);
                var obstaclesToInfect = new List<ObstacleController>();
                
                foreach (var hitCollider in hitColliders)
                {
                    ObstacleController obstacleController = hitCollider.GetComponent<ObstacleController>();
                    if (obstacleController != null)
                    {
                        obstaclesToInfect.Add(obstacleController);
                    }
                }
                
                OnShotHitObstacle?.Invoke(obstaclesToInfect);
            }
        }
        
        public void SetExplosionRadius(float scale)
        {
            explosionRadius *= scale;
        }
    }
}