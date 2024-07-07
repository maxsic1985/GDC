using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.B2B
{


    [CreateAssetMenu(fileName = nameof(SoundLoadData),
        menuName = EditorMenuConstants.SOUND + nameof(SoundLoadData))]
    public class SoundLoadData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Source;
        [Header("Positions:")]
        public Vector3 StartPosition;
        [Header("Tack List:")]
        public AudioClip[] Tracks;
        [Header("First track number")]
        public int FirstTrackNumber;
    }
}

