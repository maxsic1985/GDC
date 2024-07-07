using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using MSuhininTestovoe.B2B;
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
        private int _selectedEntity;
        private SlotView _selectedSlot;


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
                ref ItemComponent item = ref _itemComponentPool.Get(_selectedEntity);
                if (item.Count == 0) return;
                ref DropAssetComponent dropAsset = ref _dropAssetComponentPool.Add(_selectedEntity);
                dropAsset.Drop = item.Prefab;
                ref IsDropInstantiateFlag drop = ref _isDropComponentPool.Add(_selectedEntity);

                UpdateInventory(ref item);
            }
        }

        [Preserve]
        [EcsUguiClickEvent(UIConstants.BTN_SELECT_SLOT, WorldsNamesConstants.EVENTS)]
        void OnClickItem(in EcsUguiClickEvent e)
        {
            _selectedSlot = e.Sender.gameObject.GetComponent<SlotView>();
            _selectedEntity = _selectedSlot.Entity;
        }

        private void UpdateInventory(ref ItemComponent item)
        {
            item.Count -= 1;
            if (item.Count == 0)
            {
                item.Prefab = null;
                item.Sprite.sprite = null;
                item.DropType = DropType.EMPTY;
                item.CountText.text = "";
            }
            else
            {
                item.CountText.text = item.Count.ToString();
            }
        }
    }
}