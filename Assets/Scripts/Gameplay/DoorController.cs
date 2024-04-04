using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class DoorController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private Transform _targetPosition;
        public Transform TargetPosition => _targetPosition; 
        
        private Transform player;
        public Animator doorAnimator;
        public float openDistance = 5.0f;
        private bool doorOpened = false;

        private void Update()
        {
            /*
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= openDistance && !doorOpened)
            {
                doorAnimator.SetTrigger("Open");
                doorOpened = true;
            }
            */
        }

        public void Initialization()
        {
        }
    }
}