using System;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Pathfinding;



namespace MSuhininTestovoe.B2B
{
    public partial class TriggerSystem 
    {
        private EcsPool<IsEnemyFollowingComponent> _enemyIsFollowComponentPool;
        private EcsPool<IsPlayerCanAttackComponent> _isPlayerCanAtackComponenPool;
        private EcsPool<AIPathComponent> _isEnemyCanAtackComponenPool;
        private EcsPool<HealthViewComponent> _enemyHealthViewComponentPool;
        private readonly EcsCustomInject<AttackInputView> _attackInput = default;


        private void EnemyEnterToTrigger(IEcsSystems ecsSystems, EcsPool<OnTriggerEnter2DEvent> poolEnter)
        {
            foreach (var entity in _filterEnterToTrigger)
            {
                ref var eventData = ref poolEnter.Get(entity);
                var player = eventData.senderGameObject;
                var enemy = eventData.collider2D;

                if (player.GetComponent<PlayerActor>() != null
                && enemy.GetComponent<EnemyActor>() != null)
                {
                    var enemyEntity = enemy.GetComponent<EnemyActor>().Entity;
                    var playerEntity = player.GetComponent<PlayerActor>().Entity;
                    var aiDestinationSetter = enemy.GetComponent<AIDestinationSetter>();
                    if (!_enemyIsFollowComponentPool.Has(enemyEntity))
                    {
                        ref IsEnemyFollowingComponent isEnemyFollowingComponent =
                            ref _enemyIsFollowComponentPool.Add(enemyEntity);
                    }

                    var reached = enemy.GetComponent<AIPath>();
                    ref AIPathComponent isReacheded =
                        ref _isEnemyCanAtackComponenPool.Add(enemyEntity);
                    var target = player.transform;
                    aiDestinationSetter.target = target;
                    isReacheded.AIPath = reached;
                    reached.endReachedDistance = 0.5f;

                    Extensions.AddPool<HealthViewComponent>(ecsSystems, enemyEntity);
                    ref HealthViewComponent enemyHealthView = ref _enemyHealthViewComponentPool.Get(enemyEntity);
                    enemyHealthView.Value = enemy.GetComponent<EnemyActor>().GetComponent<HealthView>().Value;
          
                    ShowIconAttack(playerEntity,true);
                    poolEnter.Del(entity);
                }
            }
        }

        
        private void EmnemyExitFromTRigger(EcsPool<OnTriggerExit2DEvent> poolExit)
        {
            foreach (var entity in _filterExitFromTrigger)
            {
             
                ref var eventData = ref poolExit.Get(entity);
                var player = eventData.senderGameObject;
                var enemy = eventData.collider2D;

                if (player.GetComponent<PlayerActor>() != null
                    && enemy.GetComponent<EnemyActor>() != null)
                {
                    var enemyEntity = enemy.GetComponent<EnemyActor>().Entity;
                    var playerEntity = player.GetComponent<PlayerActor>().Entity;
                    var aiDestinationSetter = enemy.GetComponent<AIDestinationSetter>();


                    var reached = enemy.GetComponent<AIPath>();
                    aiDestinationSetter.target = enemy.gameObject.transform;
                    reached.Teleport(enemy.transform.position, true);
                    reached.endReachedDistance = Single.PositiveInfinity;
                    reached.reachedEndOfPath = false;
                    reached.endReachedDistance = 0;
                    
                    ShowIconAttack(playerEntity, false);
                    
                    _enemyIsFollowComponentPool.Del(enemyEntity);
                    _isEnemyCanAtackComponenPool.Del(enemyEntity);
                    _enemyHealthViewComponentPool.Del(enemyEntity);
                    _isPlayerCanAtackComponenPool.Del(playerEntity);
                }

                poolExit.Del(entity);
            }
        }
        
        private void ShowIconAttack(int playerEntity,bool state)
        {
            if (_isPlayerCanAtackComponenPool.Has(playerEntity) == false)
            {
                ref IsPlayerCanAttackComponent canAtack = ref _isPlayerCanAtackComponenPool.Add(playerEntity);
                canAtack.AttackInputView = _attackInput.Value;
                canAtack.AttackInputView.TriggerEnter?.Invoke(state);
            }
            else
            {
                ref IsPlayerCanAttackComponent canAtack = ref _isPlayerCanAtackComponenPool.Get(playerEntity);
                canAtack.AttackInputView = _attackInput.Value;
                canAtack.AttackInputView.TriggerEnter?.Invoke(state);
            }
        }
    }
}