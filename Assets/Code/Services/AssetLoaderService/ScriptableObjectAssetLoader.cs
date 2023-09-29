using System.Collections.Generic;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace MSuhininTestovoe.B2B
{
    public sealed class ScriptableObjectAssetLoader : IAssetLoader
    {
        private struct HandlerData
        {
            public object Address;
            public EcsWorld EcsWorld;
            public int Entity;
        }

        private readonly Dictionary<AsyncOperationHandle<ScriptableObject>, HandlerData> _handlers = new();

        private void HandlerOnCompleted(AsyncOperationHandle<ScriptableObject> handler)
        {
            HandlerData handlerData = _handlers[handler];
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                SetupScriptableObjectComponent(handlerData, handler.Result);
            }
            else
            {
                Debug.LogError($"Asset for {handlerData.Address} failed to load.");
            }

            _handlers.Remove(handler);
        }

        private void SetupScriptableObjectComponent(HandlerData handlerData, ScriptableObject data)
        {
            EcsPool<ScriptableObjectComponent> scriptableObjectComponentPool = handlerData.EcsWorld.GetPool<ScriptableObjectComponent>();
            ref ScriptableObjectComponent scriptableObjectComponent = ref scriptableObjectComponentPool.Add(handlerData.Entity);
            scriptableObjectComponent.Value = data;
        }
        public async void LoadAsset(object address, EcsWorld ecsWorld, int entity)
        {
            HandlerData handlerData = new()
            {
                Address = address,
                EcsWorld = ecsWorld,
                Entity = entity
            };
            
            AsyncOperationHandle<ScriptableObject> handler = Addressables.LoadAssetAsync<ScriptableObject>(address);
            await handler.Task;
            
            handler.Completed += HandlerOnCompleted;
            _handlers[handler] = handlerData;
        }
    }
}