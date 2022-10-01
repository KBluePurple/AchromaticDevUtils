using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Pooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        internal readonly Dictionary<GameObject, Pool> PrefabDict = new Dictionary<GameObject, Pool>();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            if (_instance == null)
            {
                var go = new GameObject("PoolManager");
                _instance = go.AddComponent<PoolManager>();
                DontDestroyOnLoad(go);

                var pools = Resources.LoadAll<Pool>("Pools");
                
                foreach (var poolSetting in pools)
                {
                    pools.Initialize();
                    _instance.PrefabDict.Add(poolSetting.prefab, poolSetting);
                }
            }
        }
        
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (_instance.PrefabDict.ContainsKey(prefab))
            {
                return _instance.PrefabDict[prefab].GetObject(position, rotation);
            }
            else
            {
                return Object.Instantiate(prefab, position, rotation);
            }
        }
        
        public static void Destroy(GameObject gameObject)
        {
            foreach (var pool in _instance.PrefabDict.Values)
            {
                if (pool.Contains(gameObject))
                {
                    pool.ReturnObject(gameObject);
                    return;
                }
            }
            
            Object.Destroy(gameObject);
        }
    }
}