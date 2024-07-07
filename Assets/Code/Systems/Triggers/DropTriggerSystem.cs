using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public partial class TriggerSystem
    {
        private EcsPool<DropComponent> _dropPool;
        private EcsPool<ItemComponent> _itemSlotPool;
        private EcsFilter _filterItem;


        private void DropEnterToTrigger(IEcsSystems ecsSystems, EcsPool<OnTriggerEnter2DEvent> poolEnter)
        {
            foreach (var entity in _filterEnterToTrigger)
            {
                ref var eventData = ref poolEnter.Get(entity);
                var player = eventData.senderGameObject;
                var dropCollider = eventData.collider2D;

                if (player.GetComponent<PlayerActor>() != null
                    && dropCollider.GetComponent<DropActor>() != null)
                {
                    var dropEntity = dropCollider.GetComponent<DropActor>().Entity;
                    var playerEntity = player.GetComponent<PlayerActor>().Entity;


                    foreach (var itemEntity in _filterItem)
                    {
                        ref ItemComponent item = ref _itemSlotPool.Get(itemEntity);
                        ref DropComponent drop = ref _dropPool.Get(dropEntity);

                        if (item.Count > 0)
                        {
                            if (item.DropType == drop.DropType)
                            {
                                UpdateInventory(ref item, ref drop);
                               
                                Object.Destroy(dropCollider.gameObject);
                                _world.DelEntity(dropEntity);
                                poolEnter.Del(entity);
                                return;
                            }
                        }
                        else
                        {
                            UpdateInventory(ref item, ref drop);
                            
                            Object.Destroy(dropCollider.gameObject);
                            _world.DelEntity(dropEntity);
                            poolEnter.Del(entity);
                            return;
                        }
                    }

                    GameObject.Destroy(dropCollider.gameObject);
                    _world.DelEntity(dropEntity);
                    poolEnter.Del(entity);
                }
            }
        }

        private static void UpdateInventory(ref ItemComponent item, ref DropComponent drop)
        {
            item.Prefab = drop.Drop;
            item.Sprite.sprite = drop.Sprite;
            item.DropType = drop.DropType;
            item.Count += 1;
            var text = item.Count > 1 ? item.Count.ToString() : " ";
            item.CountText.text = text;
           
        }
    }
}