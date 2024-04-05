using System;
using System.Collections;
using BallGame.Configs;
using BallGame.Instance;
using UnityEngine;

namespace BallGame.Gameplay
{
    public class Infection : MonoBehaviour
    {
        public event Action OnInfectionComplete;
        
        private ObjectFactory<ParticleSystem> _particleSystemFactory;
        private InfectionConfig _infectionConfig;
        
        private float infectionProgress = 0f;
        private bool isInfected = false;

        private void Update()
        {
            if (isInfected && infectionProgress < 1f)
            {
                infectionProgress += Time.deltaTime * _infectionConfig.InfectionSpeed;
                _infectionConfig.InfectedMaterial.SetFloat("_FillHeight", infectionProgress);

                if (infectionProgress >= 1f)
                {
                    StartCoroutine(ScaleUp());

                    isInfected = false;
                }
            }
        }

        private IEnumerator ScaleUp()
        {
            Vector3 initialScale = transform.localScale;
            float timer = 0f;

            while (timer <= 0.5f)
            {
                transform.localScale = Vector3.Lerp(initialScale, _infectionConfig.InfectedScale, timer);
                timer += Time.deltaTime;
                yield return null;
            }
            
            var explosionEffect = _particleSystemFactory.CreateObject();
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
            
            transform.localScale = _infectionConfig.InfectedScale;
            yield return new WaitForSeconds(0.5f);
           
            _particleSystemFactory.ReleaseObject(explosionEffect);
            gameObject.SetActive(false);
            
            OnInfectionComplete?.Invoke();
        }

        public void StartInfection()
        {
            _particleSystemFactory = ServiceLocator.GetService<ObjectFactory<ParticleSystem>>();
            _infectionConfig = ServiceLocator.GetService<ConfigService>()
                .GetConfig<InfectionConfig>(ConfigsConstants.InfectionConfigKey);
            
            GetComponent<Renderer>().material = _infectionConfig.InfectedMaterial;
            isInfected = true;
        }
    }
}