using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public class InventoryBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterInventory;
        private EcsFilter _filterSlot;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<IsInventory> _isInventoryPool;
        private EcsPool<ItemComponent> _itemSlotPool;
        private EcsPool<IsItemComponent> _isItemPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterInventory = _world
                .Filter<PrefabComponent>()
                .Inc<IsInventory>()
                .End();


            _filterSlot = _world
                .Filter<PrefabComponent>()
                .Inc<IsItemComponent>()
                .End();

            _prefabPool = _world.GetPool<PrefabComponent>();
            _isInventoryPool = _world.GetPool<IsInventory>();
            _itemSlotPool = _world.GetPool<ItemComponent>();
            _isItemPool = _world.GetPool<IsItemComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterInventory)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                var gameObject = Object.Instantiate(prefabComponent.Value);
                var canvas = GameObject.FindObjectOfType<Canvas>();
                gameObject.transform.parent = canvas.transform;
                ref var inventory = ref _isInventoryPool.Get(entity);
                inventory.Value = gameObject.GetComponent<TransformView>().gameObject;
                gameObject.transform.localPosition = Vector3.zero;

                GenerateSlots(gameObject.transform.GetChild(1));
                inventory.Value.SetActive(false);
                _prefabPool.Del(entity);
            }
        }

        private void GenerateSlots(Transform parent)
        {
            foreach (var entity in _filterSlot)
            {
                for (int i = 0; i < 4; i++)
                {
                    var newEntity = _world.NewEntity();
                    ref var itemComponent = ref _itemSlotPool.Add(newEntity);
                    ref var prefabComponent = ref _prefabPool.Get(entity);
                    var gameObject = GameObject.Instantiate(prefabComponent.Value);
                    gameObject.transform.parent = parent.transform;
                    itemComponent.Sprite = gameObject.GetComponent<SlotView>().Sprite;
                    itemComponent.CountText = gameObject.GetComponent<SlotView>().CountText;
                    itemComponent.Count = gameObject.GetComponent<SlotView>().Count;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.GetComponent<SlotView>().Entity =  newEntity ;
                }

                _isItemPool.Del(entity);
            }
        }
    }
}