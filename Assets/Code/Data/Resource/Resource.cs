using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject.ReflectionBaking.Mono.Cecil;

namespace MSuhininTestovoe.B2B
{
    [Serializable]
    public struct Resource
    {
        public string ID;
        public string Name;
        public ResourceType Type;
        public AssetReferenceSprite Sprite;
        public ScriptableObject AdditionalData;
    }
}