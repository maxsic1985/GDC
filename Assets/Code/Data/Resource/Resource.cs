using System;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.AddressableAssets;

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