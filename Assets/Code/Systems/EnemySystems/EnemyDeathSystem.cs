using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using LeopotamGroup.Globals;
using TMPro;


namespace MSuhininTestovoe.B2B
{
    public class EnemyDeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _enemyFilter;
        private EcsWorld _world;
        private EcsPool<EnemyHealthComponent> _enemyHealthComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<IsDropInstantiateFlag> _isDropComponentPool;
        private EcsPool<DropAssetComponent> _dropAssetComponentPool;
        private IPoolService _poolService;
        private int _deathEnemyCnt=0;
        [EcsUguiNamed(UIConstants.ENEMY_CNT)] readonly TextMeshProUGUI _enemyCntlabel = default;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _enemyFilter = _world
                .Filter<EnemyHealthComponent>()
                .Inc<TransformComponent>()
                .Inc<DropAssetComponent>()
                .End();
            
            _poolService = Service<IPoolService>.Get();
            _enemyHealthComponentPool = _world.GetPool<EnemyHealthComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _isDropComponentPool = _world.GetPool<IsDropInstantiateFlag>();
            _dropAssetComponentPool = _world.GetPool<DropAssetComponent>();
            _enemyCntlabel.text = _deathEnemyCnt.ToString();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyFilter)
            {
                ref TransformComponent transform = ref _transformComponentPool.Get(entity);
                ref EnemyHealthComponent health = ref _enemyHealthComponentPool.Get(entity);
                ref DropAssetComponent dropAsset = ref _dropAssetComponentPool.Get(entity);
                if (health.HealthValue<=0)
                {
                    ref IsDropInstantiateFlag drop = ref _isDropComponentPool.Add(transform.Value.gameObject.GetComponent<EnemyActor>().Entity);
                    _poolService.Return(transform.Value.gameObject);
                    health.HealthValue = 3;
                    
                    _deathEnemyCnt += 1;
                    _enemyCntlabel.text = _deathEnemyCnt.ToString();
                }
            }
        }
    }
}