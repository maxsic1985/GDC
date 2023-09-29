using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;



namespace MSuhininTestovoe.B2B
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private EcsPool<IsPlayerControlComponent> _isPlayerControlComponent;
        readonly EcsCustomInject<JoystickInputView> _joystick = default;
        private int _entity;
        private EcsFilter _filter;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();
            _playerInputComponentPool = _world.GetPool<PlayerInputComponent>();
            _isPlayerControlComponent = _world.GetPool<IsPlayerControlComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            if (_joystick.Value==null)
            {
                return;
            }
            
            foreach (int entity in _filter)
            {
                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);
                playerInputComponent.Horizontal = _joystick.Value.Horizontal;
                playerInputComponent.Vertical = _joystick.Value.Vertical;

                if (_joystick.Value.IsControl && !_isPlayerControlComponent.Has(entity))
                {
                    ref IsPlayerControlComponent playerIsControllComponent = ref _isPlayerControlComponent.Add(entity);
                }
                else if(!_joystick.Value.IsControl)
                {
                    _isPlayerControlComponent.Del(entity);
                }
            }
        }
    }
}