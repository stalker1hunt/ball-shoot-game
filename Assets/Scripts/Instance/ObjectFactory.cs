using UnityEngine;

namespace BallGame.Instance
{
    public class ObjectFactory<T> where T : Component
    {
        private ObjectPool<T> pool;

        public ObjectFactory(T prefab, int initialCapacity = 10)
        {
            pool = new ObjectPool<T>(prefab, initialCapacity);
        }

        public T CreateObject()
        {
            return pool.Get();
        }

        public void ReleaseObject(T objectToRelease)
        {
            pool.ReturnToPool(objectToRelease);
        }
        
        public void ClearPool()
        {
            pool.ClearPool();
            pool = null;
        }
    }
}