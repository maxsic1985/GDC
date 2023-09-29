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
      
    
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsInventory>().End();
            _quitBtnCommandPool = _world.GetPool<BtnQuit>();
            _isInventoryPool = _world.GetPool<IsInventory>();
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
        
        
       
       
    }
}