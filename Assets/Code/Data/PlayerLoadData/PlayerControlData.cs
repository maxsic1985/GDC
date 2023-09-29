using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.B2B
{
    [CreateAssetMenu(fileName = nameof(PlayerControlData),
        menuName = EditorMenuConstants.CREATE_PLAYER + nameof(PlayerControlData))]
    public class PlayerControlData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Player;

        [Header("Position:")] public Vector3 StartPosition;
        [Header("Player moving data:")] 
        public float MoveDistance;
        public float MoveSpeed;
        public float JumpSpeed;
        
    }
}