using UnityEngine;

namespace BallGame.Gameplay
{
    public class PathRenderer : MonoBehaviour
    {
        public Transform startPoint;
        public Transform endPoint;
        private LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            
            lineRenderer.startWidth = 1f;
            lineRenderer.endWidth = 1f;

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;

            lineRenderer.textureMode = LineTextureMode.Tile;

            lineRenderer.numCornerVertices = 5;
            lineRenderer.numCapVertices = 2;
            
            UpdatePath();
        }

        private void UpdatePath()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }

        private void Update()
        {
            if (startPoint.hasChanged || endPoint.hasChanged)
            {
                UpdatePath();
            }
        }
    }
}