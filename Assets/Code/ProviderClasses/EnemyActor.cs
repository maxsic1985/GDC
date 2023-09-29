using LeopotamGroup.Globals;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class EnemyActor : Actor
    {
        private readonly IPoolService _poolService;

        public EnemyActor()
        {
            _poolService = Service<IPoolService>.Get();
        }
        public override void Handle()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
           Debug.Log("enemy handlers");
            //_poolService.Return(gameObject);
        }
    }
}