using Leopotam.EcsLite;
using LeopotamGroup.Globals;



namespace MSuhininTestovoe.B2B
{
    public sealed class TimerRunSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<TimerComponent> _timerPool;
        private EcsPool<IsTimerFinishedComponent> _timerFinishedPool;
        private ITimeService _timeService;


        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<TimerComponent>().Exc<IsTimerPausedComponent>().End();
            _timerPool = world.GetPool<TimerComponent>();
            _timerFinishedPool = world.GetPool<IsTimerFinishedComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref TimerComponent timer = ref _timerPool.Get(entity);
                if ((timer.Value -= _timeService.DeltaTime) <= 0)
                {
                    _timerFinishedPool.Add(entity);
                    _timerPool.Del(entity);
                }
            }
        }
    }
}