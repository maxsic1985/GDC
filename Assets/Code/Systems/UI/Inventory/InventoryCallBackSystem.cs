using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using MSuhininTestovoe.B2B;
using UnityEngine;
using UnityEngine.Scripting;



namespace MSuhininTestovoe
{
    sealed class InventoryCallBackSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<BtnQuit> _quitBtnCommandPool;
        private EcsPool<IsInventory> _isInventoryPool;
        private EcsPool<IsDropInstantiateFlag> _isDropComponentPool;
        private EcsPool<DropAssetComponent> _dropAssetComponentPool;
        private EcsPool<ItemComponent> _itemComponentPool;
      
    
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsInventory>().End();
            _quitBtnCommandPool = _world.GetPool<BtnQuit>();
            _isInventoryPool = _world.GetPool<IsInventory>();
            _isDropComponentPool = _world.GetPool<IsDropInstantiateFlag>();
            _dropAssetComponentPool = _world.GetPool<DropAssetComponent>();
            _itemComponentPool = _world.GetPool<ItemComponent>();
        }
        
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.BTN_SHOW_INVENTORY, WorldsNamesConstants.EVENTS)]
        void OnClickShowInventory(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                ref IsInventory inv = ref _isInventoryPool.Get(entity);
                inv.Value.SetActive(!inv.Value.activeSelf);
            }
        }
        
        [Preserve]
        [EcsUguiClickEvent(UIConstants.BTN_DROP_FROM_INVENTORY, WorldsNamesConstants.EVENTS)]
        void OnClickDropFromInventory(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                Debug.Log("Drop");
            }
        }
        
        [Preserve]
        [EcsUguiClickEvent("TESTCLICK", WorldsNamesConstants.EVENTS)]
        void OnClickItem(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                var ent = e.Sender.gameObject.GetComponent<ItemActor>().GetComponent<SlotView>().Entity;
                Debug.Log("Itemv "+ent);
                ref ItemComponent item = ref _itemComponentPool.Get(ent);
                ref DropAssetComponent dropAsset = ref _dropAssetComponentPool.Add(ent);
                dropAsset.Drop = item.Prefab;
                ref IsDropInstantiateFlag drop = ref _isDropComponentPool.Add(ent);
            }
        }
        
    }
}