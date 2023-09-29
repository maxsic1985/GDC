using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.B2B
{
    [CreateAssetMenu(fileName = nameof(ResourcesData), 
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(ResourcesData))]
    public sealed class ResourcesData : ScriptableObject
    {
        public List<AssetReferenceT<ResourceData>> ResourceDatas;
    }
}