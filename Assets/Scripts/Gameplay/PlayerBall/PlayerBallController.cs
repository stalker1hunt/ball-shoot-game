using BallGame.Configs;
using BallGame.Instance;
using UnityEngine;

namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallController : MonoBehaviour
    {
        private ObjectFactory<BallShotController> _ballShotFactory;
        private BallShotController _currentBallShot;
        
        [SerializeField]
        private Transform _shotSpawnPoint;
        
        private float _maxShotScale = 0.3f; 
        private float _shotChargeRate = 0.5f;
        private float _minPlayerScale = 0.5f;
        
        private float currentShotScale = 0f;
        private Transform _target;

        public void Setup(PlayerBallConfig playerBallConfig)
        {
            _ballShotFactory = ServiceLocator.GetService<ObjectFactory<BallShotController>>();
            
            _target = playerBallConfig.TargetPosition;
            _maxShotScale = playerBallConfig.MaxShotScale;
            _shotChargeRate = playerBallConfig.ShotChargeRate;
            _minPlayerScale = playerBallConfig.MinPlayerScale;
        }
        
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    CreateShot();
                }

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (_currentBallShot != null)
                    {
                        ChargeShot();
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    ReleaseShot();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CreateShot();
                }

                if (Input.GetMouseButton(0))
                {
                    if (_currentBallShot != null)
                    {
                        ChargeShot();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    ReleaseShot();
                }
            }
        }

        private void CreateShot()
        {
            if (transform.localScale.x > _minPlayerScale)
            {
                _currentBallShot = _ballShotFactory.CreateObject();
                _currentBallShot.transform.position = _shotSpawnPoint.position;
                currentShotScale = 0f;
            }
        }

        private void ChargeShot()
        {
            currentShotScale = Mathf.Min(currentShotScale + _shotChargeRate * Time.deltaTime, _maxShotScale);

            float scaleDecrease = _shotChargeRate * Time.deltaTime;
            Vector3 newPlayerScale = transform.localScale - new Vector3(scaleDecrease, scaleDecrease, scaleDecrease);

            if (newPlayerScale.x >= _minPlayerScale)
            {
                transform.localScale = newPlayerScale;
                _currentBallShot.transform.localScale = new Vector3(currentShotScale, currentShotScale, currentShotScale);
            }
            else
            {
                ReleaseShot();
            }
        }
        
        private void ReleaseShot()
        {
            if (_currentBallShot != null)
            {
                _currentBallShot.GetComponent<BallShotController>().Initiate(_target);
                _currentBallShot.SetExplosionRadius(1);
                _currentBallShot = null;
            }
        }
    }
}