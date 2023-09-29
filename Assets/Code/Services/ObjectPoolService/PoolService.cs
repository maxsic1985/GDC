using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;



namespace MSuhininTestovoe.B2B
{
    public class PoolService : IPoolService
    {
        private readonly Dictionary<GameObjectsTypeId, Pool> _poolsRepository;
        private readonly List<Task> _tasks = new();
        public int Count => _poolsRepository.Count;
        private Transform PoolServiceTransform => _poolServiceGameObject.transform;
        private int _index;
        private GameObject _poolServiceGameObject;
        public int Capacity { get; set; }
        public PoolService()
        {
            _poolsRepository = new Dictionary<GameObjectsTypeId, Pool>();
        }

        public async Task Initialize()
        {
            _poolServiceGameObject = new GameObject("PoolService");
            foreach (GameObjectsTypeId pooledObject in
                ((GameObjectsTypeId[]) Enum.GetValues(typeof(GameObjectsTypeId))).Where(x => (int)x <= 100))
            {
                _tasks.Add(LoadAssets(pooledObject));
            }

            await Task.WhenAll(_tasks);
        }

        private async Task<GameObject> LoadAssets(GameObjectsTypeId gameObjectsTypeId)
        {
            string name = Enum.GetName(typeof(GameObjectsTypeId), gameObjectsTypeId);
            
            AsyncOperationHandle<PoolData> soResult = Addressables.LoadAssetAsync<PoolData>(name);
            await soResult.Task;

            AsyncOperationHandle<GameObject> goResult =
                Addressables.LoadAssetAsync<GameObject>(soResult.Result.PooledObject.RuntimeKey);
            await goResult.Task;

            Add(gameObjectsTypeId, goResult.Result, soResult.Result.Capacity);
            Capacity = soResult.Result.Capacity;
            return goResult.Result;
        }

        public void Add(GameObjectsTypeId type, GameObject pooledObject, int capacity)
        {
            _poolsRepository.Add(type, new Pool(type, pooledObject, capacity));
        }

        public void Clear()
        {
            _poolsRepository.Clear();
        }

        public void Clear(GameObjectsTypeId gameObjectsTypeId)
        {
            if (_poolsRepository.TryGetValue(gameObjectsTypeId, out Pool pool))
            {
                pool.ClearPool();
            }
        }

        public void Return(GameObject gameObject)
        {
            gameObject.transform.parent = PoolServiceTransform;
            gameObject.SetActive(false);
        }

        public GameObject Get(GameObjectsTypeId gameObjectsTypeId)
        {
            GameObject pooledObject = default;

            if (_poolsRepository.TryGetValue(gameObjectsTypeId, out Pool pool))
            {
                if (pool.Count == 0)
                {
                    _index = 0;
                    for (int i = 0; i < pool.Capacity; i++)
                    {
                        pooledObject = Object.Instantiate(pool.PooledObject, PoolServiceTransform);
                        pooledObject.name = $"{gameObjectsTypeId.ToString()}({i})";
                        pooledObject.SetActive(false);
                        pool.SetPooledObject(pooledObject);
                        _index = i;
                    }
                }

                pooledObject = pool.GetFirst();

                if (pooledObject.activeInHierarchy)
                {
                    _index = pool.Count - 1;
                    GameObject additional = Object.Instantiate(pool.PooledObject, PoolServiceTransform);
                    additional.name = $"{gameObjectsTypeId.ToString()}({++_index})";
                    pool.SetPooledObject(additional);
                    return additional;
                }

                pooledObject = pool.GetPooledObject();
                pooledObject.SetActive(true);
                pool.SetPooledObject(pooledObject);
            }

            return pooledObject;
        }
    }
}