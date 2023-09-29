using LeopotamGroup.Globals;

namespace MSuhininTestovoe.B2B
{
    public class ItemActor : Actor
    {
        private readonly IPoolService _poolService;

        public ItemActor()
        {
            _poolService = Service<IPoolService>.Get();
        }
        public override void Handle()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _poolService.Return(gameObject);
        }
    }
}