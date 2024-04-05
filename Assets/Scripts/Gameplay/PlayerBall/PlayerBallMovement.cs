using System.Collections;
using UnityEngine;

namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallMovement : MonoBehaviour
    {
        [SerializeField]
        private float _jumpHeight = 0.5f;
        [SerializeField]
        private float _jumpDistance = 1f;
        [SerializeField]
        private float _jumpSpeed = 2f;

        [SerializeField]
        private LayerMask _obstacleLayer;

        private Transform _globalTarget;

        private bool isJumping;

        public void TryJumpToNextTarget(Transform target)
        {
            _globalTarget = target;
           
            if(isJumping) return;

            Vector3 pathStart = transform.position;
            Vector3 pathEnd = GetNextJumpTargetOnPath(_globalTarget);

            float playerBallSize = transform.localScale.x;
                
            if (IsPathClear(pathStart, pathEnd, _jumpDistance, playerBallSize)) 
            {
                if(Vector3.Distance(pathEnd, _globalTarget.position) <= _jumpDistance) 
                {
                    Debug.Log("Reached the global target!");
                    return;
                }
                StartCoroutine(JumpToNextTarget(pathEnd));
            }
            else 
            {
                Debug.Log("Obstacle detected. Can't jump further.");
            }
        }

        private IEnumerator JumpToNextTarget(Vector3 endPosition)
        {
            isJumping = true;
            Vector3 startPosition = transform.position;

            float timeElapsed = 0;
            float journeyLength = Vector3.Distance(startPosition, endPosition);

            while (timeElapsed < journeyLength / _jumpSpeed)
            {
                float interp = timeElapsed / (journeyLength / _jumpSpeed);
                Vector3 newPos = Vector3.Lerp(startPosition, endPosition, interp);
                newPos.y = _jumpHeight + Mathf.Sin(interp * Mathf.PI);
                transform.position = newPos;

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            isJumping = false;

            TryJumpToNextTarget(_globalTarget);
        }

        private bool IsPathClear(Vector3 pathStart, Vector3 pathEnd, float pathWidth, float playerSize)
        {
            float distance = Vector3.Distance(pathStart, pathEnd);
            Vector3 direction = (pathEnd - pathStart).normalized;
    
            Vector3 halfExtents = new Vector3(pathWidth / 2, playerSize / 2, distance / 2);
    
            Quaternion orientation = Quaternion.LookRotation(direction);
            Vector3 center = pathStart + direction * distance / 2;

            bool isBlocked = Physics.BoxCast(center, halfExtents, direction, orientation, distance, _obstacleLayer);

            return !isBlocked;
        }
        
        private Vector3 GetNextJumpTargetOnPath(Transform globalTarget) 
        {
            Vector3 directionToGlobalTarget = (globalTarget.position - transform.position).normalized;
            Vector3 nextJumpTarget = transform.position + directionToGlobalTarget * _jumpDistance;
            nextJumpTarget.y = transform.position.y; 
    
            return nextJumpTarget;
        }
    }
}
