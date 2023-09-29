using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using Pathfinding;
using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public class EnemyInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsEnemyComponent> _isEnemyPool;
        private EcsPool<EnemyStartPositionComponent> _enemyStartPositionComponentPool;
        private EcsPool<EnemyStartRotationComponent> _enemyStartRotationComponentPool;
        private EcsPool<EnemySpawnComponent> _enemySpawnComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<EnemyHealthComponent> _enemyHealthComponentPool;
        private EcsPool<AIDestanationComponent> _aiDestanationComponenPool;
        private EcsPool<AIPathComponent> _aIpathComponenPool;
        private EcsPool<BoxColliderComponent> _enemyBoxColliderComponentPool;
        private EcsPool<DropAssetComponent> _dropComponentPool;
        private IPoolService _poolService;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _poolService = Service<IPoolService>.Get();
            _filter = _world
                .Filter<IsEnemyComponent>()
                .Inc<ScriptableObjectComponent>()
                .End();
            
            _isEnemyPool = _world.GetPool<IsEnemyComponent>();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _enemySpawnComponentPool = _world.GetPool<EnemySpawnComponent>();
            _enemyStartRotationComponentPool = _world.GetPool<EnemyStartRotationComponent>();
            _enemyStartPositionComponentPool = _world.GetPool<EnemyStartPositionComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _enemyHealthComponentPool = _world.GetPool<EnemyHealthComponent>();
            _enemyBoxColliderComponentPool = _world.GetPool<BoxColliderComponent>();
            _aiDestanationComponenPool = _world.GetPool<AIDestanationComponent>();
            _dropComponentPool = _world.GetPool<DropAssetComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is EnemyData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);

                    CreateEnemy(entity, dataInit);
                    SpawnEnemy(dataInit);
                }

                _scriptableObjectPool.Del(entity);
            }
        }


        private void CreateEnemy(int entity, EnemyData dataInit)
        {
            for (int i = 0; i < _poolService.Capacity; i++)
            {
                var newEntity = _world.NewEntity();
                GameObject pooled = _poolService.Get(GameObjectsTypeId.Enemy);
                pooled.gameObject.GetComponent<IActor>().AddEntity(newEntity);

                ref EnemySpawnComponent spawn = ref _enemySpawnComponentPool.Add(newEntity);
                ref TransformComponent transformComponent = ref _transformComponentPool.Add(newEntity);
                ref EnemyHealthComponent enemyHealth = ref _enemyHealthComponentPool.Add(newEntity);
                ref BoxColliderComponent enemyBoxColliderComponent = ref _enemyBoxColliderComponentPool.Add(newEntity);
                ref EnemyStartPositionComponent enemyStartPositionComponent =
                    ref _enemyStartPositionComponentPool.Add(newEntity);
                ref EnemyStartRotationComponent enemyStartRotationComponent =
                    ref _enemyStartRotationComponentPool.Add(newEntity);
                ref AIDestanationComponent aiDestanationComponent = ref _aiDestanationComponenPool.Add(newEntity);
                ref DropAssetComponent dropAssetComponent = ref _dropComponentPool.Add(newEntity);

                spawn.SpawnLenght = dataInit.CountForInstantiate;
                transformComponent.Value = pooled.gameObject.GetComponent<TransformView>().Transform;
                enemyHealth.HealthValue = (int) pooled.GetComponent<HealthView>().Value.size.x;
                enemyBoxColliderComponent.ColliderValue = pooled.GetComponent<BoxCollider>();
                enemyStartPositionComponent.Value = new List<Vector3>();
                enemyStartRotationComponent.Value = new List<Vector3>();
                aiDestanationComponent.AIDestinationSetter = pooled.gameObject.GetComponent<AIDestinationSetter>();
                var index = Extensions.GetRandomDigit(0, dataInit.DropPrefabs.Count);
                dropAssetComponent.Drop = dataInit.DropPrefabs[index];
          
                foreach (var pos in dataInit.StartPositions)
                {
                    enemyStartPositionComponent.Value.Add(pos);
                }

                foreach (var pos in dataInit.StartRotation)
                {
                    enemyStartRotationComponent.Value.Add(pos);
                }
                
                _poolService.Return(pooled);
            }

            _isEnemyPool.Del(entity);
            _loadPrefabPool.Del(entity);
        }


        private void SpawnEnemy(EnemyData dataInit)
        {
            var positionIndex = Extensions
                .GetUniqeRandomArray(dataInit.CountForInstantiate, 0, dataInit.StartPositions.Count);

            for (int i = 0; i < dataInit.CountForInstantiate; i++)
            {
                GameObject pooled = _poolService.Get(GameObjectsTypeId.Enemy);
                var entity = pooled.gameObject.GetComponent<IActor>().Entity;

                ref EnemyStartPositionComponent enemyStartPositionComponent =
                    ref _enemyStartPositionComponentPool.Get(entity);
                ref EnemyStartRotationComponent enemyStartRotationComponent =
                    ref _enemyStartRotationComponentPool.Get(entity);

                pooled.transform.position = enemyStartPositionComponent.Value[positionIndex[i]];
                pooled.transform.rotation = Quaternion.EulerAngles(enemyStartRotationComponent.Value[positionIndex[i]]);
            }
        }
    }
}