using System.Collections.Generic;
using UnityEngine;

namespace BallGame.Instance
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> objects = new Queue<T>();
        private readonly T prefab;

        public ObjectPool(T prefab, int initialCapacity = 10)
        {
            this.prefab = prefab;
            for (int i = 0; i < initialCapacity; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveAtStart = false)
        {
            var newObject = Object.Instantiate(prefab);
            newObject.gameObject.SetActive(isActiveAtStart);
            objects.Enqueue(newObject);
            return newObject;
        }

        public T Get()
        {
            if (objects.Count == 0)
            {
                CreateObject(true);
            }

            var instance = objects.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            objects.Enqueue(instance);
        }
    }
}