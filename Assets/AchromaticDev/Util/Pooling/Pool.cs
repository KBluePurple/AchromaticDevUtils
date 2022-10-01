using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AchromaticDev.Util.Pooling
{
    [UnityEngine.CreateAssetMenu(fileName = "New Pool Settings", menuName = "AchromaticDev/Pooling", order = 0)]
    public class Pool : ScriptableObject
    {
        public GameObject prefab = null;
        public int initialPoolSize = 0;
        public bool clearOnSceneChange = false;
        
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private GameObject _poolParent = null;
        
        public int PoolSize => _pool.Count;
        
        public void Initialize(Transform parent)
        {
            _poolParent = new GameObject(prefab.name + " Pool");
            _poolParent.transform.parent = parent;
            
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, _poolParent.transform);
                obj.SetActive(false);
                _pool.Enqueue(obj);
            }
            
            if (clearOnSceneChange)
            {
                SceneManager.sceneUnloaded += SceneUnloaded;
            }
        }
        
        private void SceneUnloaded(Scene scene)
        {
            while (_pool.Count > 0)
            {
                Destroy(_pool.Dequeue());
            }
        }
        
        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            GameObject obj;
            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else
            {
                obj = Instantiate(prefab, _poolParent.transform);
            }
            
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            
            return obj;
        }
        
        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
        
        public bool Contains(GameObject obj)
        {
            return _pool.Contains(obj);
        }
    }
}