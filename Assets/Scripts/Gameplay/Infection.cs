using System.Collections;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class Infection : MonoBehaviour
    {
        public Material infectedMaterial;
        public float infectionSpeed = 1f;
        public Vector3 infectedScale = new Vector3(1.2f, 1.2f, 1.2f);
        public ParticleSystem explosionEffect;

        private float infectionProgress = 0f;
        private bool isInfected = false;

        void Update()
        {
            if (isInfected && infectionProgress < 1f)
            {
                infectionProgress += Time.deltaTime * infectionSpeed;
                infectedMaterial.SetFloat("_FillHeight", infectionProgress);

                if (infectionProgress >= 1f)
                {
                    StartCoroutine(ScaleUp());

                    explosionEffect.Play();

                    isInfected = false;
                }
            }
        }

        private IEnumerator ScaleUp()
        {
            Vector3 initialScale = transform.localScale;
            float timer = 0f;

            while (timer <= 1f)
            {
                transform.localScale = Vector3.Lerp(initialScale, infectedScale, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = infectedScale;
        }

        public void StartInfection()
        {
            GetComponent<Renderer>().material = infectedMaterial;
            isInfected = true;
        }
    }
}