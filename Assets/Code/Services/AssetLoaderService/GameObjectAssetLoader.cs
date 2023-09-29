using System.Collections.Generic;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



namespace MSuhininTestovoe.B2B
{
    public sealed class GameObjectAssetLoader : IAssetLoader
    {
        private struct HandlerData
        {
            public object Address;
            public EcsWorld EcsWorld;
            public int Entity;
        }
        
        private readonly Dictionary<AsyncOperationHandle<GameObject>, HandlerData> _handlers = new();
        
        public async void LoadAsset(object address, EcsWorld ecsWorld, int entity)
        {
            HandlerData handlerData = new()
                              {
                                  Address = address,
                                  EcsWorld = ecsWorld,
                                  Entity = entity
                              };

            AsyncOperationHandle<GameObject> handler = Addressables.LoadAssetAsync<GameObject>(address);
            await handler.Task;
           
            handler.Completed += HandleOnComplete;
            
            _handlers[handler] = handlerData;
        }

        private void HandleOnComplete(AsyncOperationHandle<GameObject> handler)
        {
            HandlerData handlerData = _handlers[handler];
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                EcsPool<PrefabComponent> pool = handlerData.EcsWorld.GetPool<PrefabComponent>();
                ref PrefabComponent component = ref pool.Add(handlerData.Entity);
                component.Value = handler.Result;
            }
            else
            {
                Debug.LogError($"Asset for {handlerData.Address} failed to load.");
            }

            _handlers.Remove(handler);
        }
    }
}
