using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public class PlayerBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PlayerStartPositionComponent> _playerStartPositionComponentPool;
        private EcsPool<BoxColliderComponent> _playerBoxColliderComponentPool;
        private EcsPool<PlayerRigidBodyComponent> _playerRigidBodyComponentPool;
        private EcsPool<HealthViewComponent> _playerHealthViewComponentPool;
        private PlayerSharedData _sharedData;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<IsPlayerComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _playerStartPositionComponentPool = world.GetPool<PlayerStartPositionComponent>();
            _playerBoxColliderComponentPool = world.GetPool<BoxColliderComponent>();
            _playerRigidBodyComponentPool = world.GetPool<PlayerRigidBodyComponent>();
            _playerHealthViewComponentPool = world.GetPool<HealthViewComponent>();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;

        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PrefabComponent prefabComponent = ref _prefabPool.Get(entity);
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref PlayerStartPositionComponent playerPosition = ref _playerStartPositionComponentPool.Get(entity);
                ref PlayerRigidBodyComponent playerRigidBodyComponent = ref _playerRigidBodyComponentPool.Add(entity);
                ref BoxColliderComponent boxColliderComponent = ref _playerBoxColliderComponentPool.Add(entity);
                ref HealthViewComponent healthView = ref _playerHealthViewComponentPool.Add(entity);

                GameObject gameObject = Object.Instantiate(prefabComponent.Value);
                transformComponent.Value = gameObject.GetComponent<TransformView>().Transform;
                gameObject.transform.position = playerPosition.Value;
                gameObject.GetComponent<IActor>().AddEntity(entity);
                boxColliderComponent.ColliderValue = gameObject.GetComponent<BoxCollider>();
                playerRigidBodyComponent.PlayerRigidbody = gameObject.GetComponent<Rigidbody2D>();
                healthView.Value = gameObject.GetComponent<HealthView>().Value;
                _prefabPool.Del(entity);
            }
        }
    }
}