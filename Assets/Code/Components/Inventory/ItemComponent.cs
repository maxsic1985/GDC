using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MSuhininTestovoe.B2B
{
    public struct ItemComponent
    {
        public DropType DropType;
        public Image Sprite;
        public int Count;
        public TMP_Text CountText;
        public GameObject Prefab;
    }
}