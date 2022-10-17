using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Pooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        internal readonly Dictionary<GameObject, Pool> PrefabDict = new Dictionary<GameObject, Pool>();
        internal readonly Dictionary<GameObject, PoolObject> PoolObjectCache = new Dictionary<GameObject, PoolObject>();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            if (Instance != null) return;
            
            Initialize(true);

            var pools = Resources.LoadAll<Pool>("Pools");
                
            foreach (var poolSetting in pools)
            {
                poolSetting.Initialize(Instance.transform);
                Instance.PrefabDict.Add(poolSetting.prefab, poolSetting);
            }
        }
        
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (Instance.PrefabDict.ContainsKey(prefab))
            {
                return Instance.PrefabDict[prefab].GetObject(prefab, position, rotation, parent);
            }

            Instance.PrefabDict.Add(prefab, ScriptableObject.CreateInstance<Pool>());
            Instance.PrefabDict[prefab].prefab = prefab;
            Instance.PrefabDict[prefab].Initialize(Instance.transform);
            return Instance.PrefabDict[prefab].GetObject(prefab, position, rotation, parent);
        }
        
        public static GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public static void Destroy(GameObject gameObject)
        {
            var prefab = Instance.PoolObjectCache[gameObject].pool.prefab;
            
            if (Instance.PrefabDict.ContainsKey(prefab))
            {
                Instance.PrefabDict[prefab].ReturnObject(gameObject);
            }
            else
            {
                Debug.LogWarning($"PoolManager: {prefab.name} is not in the pool dictionary.");
                Object.Destroy(gameObject);
            }
        }
    }
}