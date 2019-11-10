using System.Collections.Generic;
using UnityEngine;

namespace HCFramework
{
    public class ObjectPool<T> where T : MonoBehaviour,IPoolable
    {
        // every object that can be pooled should have a script that implements Ipoolable interface
        List<T> objectPool = new List<T>();
        int objectsInPool, maxPool = 100;
        T poolPrefab;
        public ObjectPool(int poolSize, string resourceFilePath)
        {
            poolPrefab = Resources.Load<T>(resourceFilePath);
            maxPool = poolSize;
        }

        public T GetFromPool<BT>() where BT : T, new()
        {
            T poolObject = default(T);
            if (objectPool.Count == 0 && objectsInPool <= maxPool)
            {
                objectsInPool++;
                poolObject = GameObject.Instantiate(poolPrefab.gameObject) as T;
            }
            else
            {
                foreach (T obj in objectPool)
                {
                    if (obj is BT)
                    {
                        poolObject = obj;
                        objectPool.Remove(poolObject);
                        break;
                    }
                }
            }
            if (poolObject == null)
            {
                Debug.LogError("Pool Size not enough increade pool size");
                objectsInPool++;
                poolObject = (T)new BT();

            }
            return poolObject;
        }
        public void ReturnToPool(T poolObject)
        {
            objectPool.Add(poolObject);
            poolObject.Reset();
        }

    }
}