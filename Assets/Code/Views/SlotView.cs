using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MSuhininTestovoe.B2B
{
    public sealed class SlotView : BaseView
    {
        [SerializeField] private Image _sprite;
        public int Entity;
        [SerializeField] private TMP_Text countTextText;
        [SerializeField] private int _count;

        public Image Sprite => _sprite;
       
        public TMP_Text CountText => countTextText;
        public int Count => _count;
    }
    
}