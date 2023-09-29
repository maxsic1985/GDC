using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public sealed class TransformView : BaseView
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
    }
}