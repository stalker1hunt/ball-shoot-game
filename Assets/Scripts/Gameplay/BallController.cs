using UnityEngine;

namespace BallGame.Gameplay
{
    public class BallController : MonoBehaviour
    {
        public ShotController shotPrefab;
        public Transform shotSpawnPoint;
        public Transform target;
        public float maxShotScale = 0.3f; 
        public float shotChargeRate = 0.5f;
        public float minPlayerScale = 0.5f;

        private float currentShotScale = 0f;
        private ShotController currentShot;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateShot();
            }

            if (Input.GetMouseButton(0))
            {
                if (currentShot != null)
                {
                    ChargeShot();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                ReleaseShot();
            }
        }

        private void CreateShot()
        {
            if (transform.localScale.x > minPlayerScale)
            {
                currentShot = Instantiate(shotPrefab, shotSpawnPoint.position, Quaternion.identity);
                currentShotScale = 0f;
            }
        }

        private void ChargeShot()
        {
            currentShotScale = Mathf.Min(currentShotScale + shotChargeRate * Time.deltaTime, maxShotScale);

            float scaleDecrease = shotChargeRate * Time.deltaTime;
            Vector3 newPlayerScale = transform.localScale - new Vector3(scaleDecrease, scaleDecrease, scaleDecrease);

            if (newPlayerScale.x >= minPlayerScale)
            {
                transform.localScale = newPlayerScale;
                currentShot.transform.localScale = new Vector3(currentShotScale, currentShotScale, currentShotScale);
            }
            else
            {
                ReleaseShot();
            }
        }

        private void ReleaseShot()
        {
            if (currentShot != null)
            {
                currentShot.GetComponent<ShotController>().Initiate(target);
                currentShot.SetExplosionRadius(1);
                currentShot = null;
            }
        }
    }
}