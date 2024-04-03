using UnityEngine;

namespace BallGame.Gameplay
{
    public class PathRenderer : MonoBehaviour
    {
        public Transform playerTransform;
        public Transform doorTransform;
        private GameObject pathCube;

        private void Start()
        {
            pathCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(pathCube.GetComponent<BoxCollider>());

            pathCube.GetComponent<Renderer>().material.color = Color.yellow;

            UpdatePath();
        }

        private void UpdatePath()
        {
            Vector3 startPoint = playerTransform.position;
            Vector3 endPoint = doorTransform.position;

            Vector3 direction = (endPoint - startPoint).normalized;
            float distance = Vector3.Distance(startPoint, endPoint);

            pathCube.transform.position = startPoint + direction * distance / 2;
            pathCube.transform.localScale = new Vector3(1f, 0.1f, distance);
            pathCube.transform.LookAt(doorTransform.position); // Куб "дивиться" на двері
        }

        private void Update()
        {
            if (playerTransform.hasChanged || doorTransform.hasChanged)
            {
                UpdatePath();
                playerTransform.hasChanged = false;
                doorTransform.hasChanged = false;
            }
        }
    }
}
