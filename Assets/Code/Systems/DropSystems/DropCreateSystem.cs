using Leopotam.EcsLite;
using UnityEngine;
using Object = UnityEngine.Object;


namespace MSuhininTestovoe.B2B
{
    public class DropCreateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsFilter _playerFilter;
        private EcsWorld _world;
        private EcsPool<PrefabComponent> _loadPrefabPool;
        private EcsPool<DropAssetComponent> _dropPool;
        private EcsPool<IsDropInstantiateFlag> _isDropInstantiateFlag;
        private EcsPool<DropComponent> _isDropPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<DropAssetComponent>()
                .Inc<IsDropInstantiateFlag>()
                .End();

            _playerFilter = _world
                .Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();

            _dropPool = _world.GetPool<DropAssetComponent>();
            _isDropInstantiateFlag = _world.GetPool<IsDropInstantiateFlag>();
            _isDropPool = _world.GetPool<DropComponent>();
            _loadPrefabPool = _world.GetPool<PrefabComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref DropAssetComponent dropAsset = ref _dropPool.Get(entity);
                ref PrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                loadPrefabFromPool.Value = dropAsset.Drop;

                var newEntity = _world.NewEntity();
                GameObject dropObject = Object.Instantiate(loadPrefabFromPool.Value);

                dropObject.GetComponent<DropActor>().AddEntity(newEntity);
                ref DropComponent drop = ref _isDropPool.Add(newEntity);
                drop.Drop = dropAsset.Drop;

                drop.DropType = dropObject.GetComponent<DropActor>().DropType;
                drop.Sprite = dropObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;

                var playerEntity = _playerFilter.GetRawEntities()[0];
                dropObject.transform.position = _world.GetPool<TransformComponent>().Get(playerEntity).Value.position +
                                                GameConstants.DROP_OFFSET;

                _loadPrefabPool.Del(entity);
                _isDropInstantiateFlag.Del(entity);
                _dropPool.Del(entity);
            }
        }
    }
}