using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.B2B
{
    [CreateAssetMenu(fileName = nameof(PlayerSharedData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(PlayerSharedData), order = 2)]
    public sealed class PlayerSharedData : ScriptableObject
    {
        [SerializeField] private PlayerCharacteristic _player;
       

        public PlayerCharacteristic GetPlayerCharacteristic => _player;
     
    }
}