using BallGame.Gameplay.PlayerBall;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class PathRenderer : MonoBehaviour, IInitialization
    {
        [SerializeField]
        public Transform _playerTransform;
        [SerializeField]
        public Transform _doorTransform;

        private GameObject _pathCube;
        
        private PlayerBallController _playerBallController;

        public void Initialization()
        {
            _pathCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(_pathCube.GetComponent<BoxCollider>());
            _pathCube.GetComponent<Renderer>().material.color = Color.yellow;
            
            UpdatePath();
            _playerBallController = ServiceLocator.GetService<PlayerBallController>();
            _playerBallController.OnSizeChanged += HandleSizeChanged;
        }

        private void HandleSizeChanged(float newSize)
        {
            _pathCube.transform.localScale = new Vector3(newSize, _pathCube.transform.localScale.y, _pathCube.transform.localScale.z);
        }

        private void UpdatePath()
        {
            Vector3 startPoint = _playerTransform.position;
            Vector3 endPoint = _doorTransform.position;

            Vector3 direction = (endPoint - startPoint).normalized;
            float distance = Vector3.Distance(startPoint, endPoint);

            _pathCube.transform.position = startPoint + direction * distance / 2;
            _pathCube.transform.localScale = new Vector3(1f, 0.1f, distance);
            _pathCube.transform.LookAt(_doorTransform.position);
        }
    }
}
