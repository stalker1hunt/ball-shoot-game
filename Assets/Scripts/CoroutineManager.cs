using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame
{
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;

        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var managerObject = new GameObject("CoroutineManager");
                    DontDestroyOnLoad(managerObject);
                    _instance = managerObject.AddComponent<CoroutineManager>();
                }

                return _instance;
            }
        }

        private Dictionary<string, Coroutine> _activeCoroutines = new Dictionary<string, Coroutine>();

        public void StartInfectionCoroutine(string coroutineId, IEnumerator coroutine)
        {
            if (_activeCoroutines.ContainsKey(coroutineId))
            {
                StopCoroutine(_activeCoroutines[coroutineId]);
                _activeCoroutines.Remove(coroutineId);
            }

            Coroutine newCoroutine = StartCoroutine(coroutine);
            _activeCoroutines[coroutineId] = newCoroutine;
        }

        public void StopInfectionCoroutine(string coroutineId)
        {
            if (_activeCoroutines.TryGetValue(coroutineId, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                _activeCoroutines.Remove(coroutineId);
            }
        }
    }
}