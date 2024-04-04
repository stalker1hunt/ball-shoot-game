using System.Collections;
using UnityEngine;

namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallMovement : MonoBehaviour
    {
        public float jumpHeight = 1f;
        public float jumpDistance = 1f;
        public float jumpSpeed = 2f;
        public LayerMask obstacleLayer;

        private bool isJumping;
        private Vector3 nextJumpTarget;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Vector3 pathStart = transform.position;
                Vector3 pathEnd = transform.position + transform.forward * jumpDistance;

                if (IsPathClear(pathStart, pathEnd, jumpDistance))
                {
                    nextJumpTarget = pathEnd;
                    StartCoroutine(JumpToNextTarget());
                }
            }
        }

        private IEnumerator JumpToNextTarget()
        {
            isJumping = true;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(nextJumpTarget.x, startPosition.y, nextJumpTarget.z);

            float timeElapsed = 0;
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float jumpHeightAdjusted = jumpHeight + startPosition.y;

            while (timeElapsed < journeyLength / jumpSpeed)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / (journeyLength / jumpSpeed));

                transform.position = new Vector3(transform.position.x, jumpHeightAdjusted + Mathf.Sin((timeElapsed / (journeyLength / jumpSpeed)) * Mathf.PI), transform.position.z);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            isJumping = false;
        }

        private bool IsPathClear(Vector3 pathStart, Vector3 pathEnd, float pathWidth)
        {
            float distance = Vector3.Distance(pathStart, pathEnd);
            Vector3 direction = (pathEnd - pathStart).normalized;
            Vector3 halfExtents = new Vector3(pathWidth / 2, 0.1f, distance / 2);
            Quaternion orientation = Quaternion.LookRotation(direction);
            Vector3 center = pathStart + direction * distance / 2;

            bool isBlocked = Physics.BoxCast(center, halfExtents, direction, orientation, distance, obstacleLayer);

            return !isBlocked;
        }
    }
}