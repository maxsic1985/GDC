using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _playerFilter;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private EcsPool<IsPlayerControlComponent> _isPlayerMoveComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private ITimeService _timeService;
        private   PlayerSharedData _sharedData;
        private Vector3 playerPosition;

        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _playerFilter = world.Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .Inc<IsPlayerControlComponent>()
                .End();
            _playerInputComponentPool = world.GetPool<PlayerInputComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _isPlayerMoveComponentPool = world.GetPool<IsPlayerControlComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);

                if (_isPlayerMoveComponentPool.Has(entity))
                    PlayerMoving(ref transformComponent,
                        ref playerInputComponent);
            }
        }

      
        private void PlayerMoving(ref TransformComponent transformComponent, ref PlayerInputComponent inputComponent)//,ref DestinationComponent destinationComponent)
        {
            Vector3 direction = Vector3.up * inputComponent.Vertical + Vector3.right * inputComponent.Horizontal;
           
         transformComponent.Value.position = Vector3.Lerp( transformComponent.Value.position,
             transformComponent.Value.position+direction,
             _sharedData.GetPlayerCharacteristic.Speed * _timeService.DeltaTime);
        }
    }
}