using UnityEngine;



namespace MSuhininTestovoe.B2B
{
    public sealed class CameraView : BaseView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TransformView _transformView;

        public TransformView TransformView => _transformView;
        public Camera Camera => _camera;
    }
}