using System;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public sealed class AttackInputView : BaseView
    {
        public Action<bool> TriggerEnter;
        [SerializeField] private GameObject _attackBtn;
        public GameObject AttackBtn => _attackBtn;
        

        private void OnDestroy()
        {
            TriggerEnter -= ShowIconAttack;
        }
        private void Awake()
        {
            TriggerEnter += ShowIconAttack;
            ShowIconAttack(false);
        }

        public void ShowIconAttack(bool show)
        {
            if (show) _attackBtn.SetActive(true);
            else _attackBtn.SetActive(false);
        }

    }
}