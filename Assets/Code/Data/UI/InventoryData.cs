using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.B2B
{
    [CreateAssetMenu(fileName = nameof(InventoryData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(InventoryData))]
    public class InventoryData : ScriptableObject
    {
        [Header("Inventory")] 
        public AssetReferenceGameObject Inventory; 
     

    }
}