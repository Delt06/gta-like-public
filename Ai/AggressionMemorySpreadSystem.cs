using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Health;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai
{
    public class AggressionMemorySpreadSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly Collider[] _colliders = new Collider[16];
        private readonly IDamageTypesPriorityService _damageTypesPriorityService;

        public AggressionMemorySpreadSystem(IDamageTypesPriorityService damageTypesPriorityService) =>
            _damageTypesPriorityService = damageTypesPriorityService;

        public void Run(EcsSystems systems)
        {
            var otherEntityFilter = Filter<AggressionMemory>().End();

            foreach (var i in Filter<AggressionMemorySpread>()
                         .Inc<AggressionMemory>()
                         .IncTransform()
                         .End())
            {
                ref var aggressionMemorySpread = ref Get<AggressionMemorySpread>(i);
                aggressionMemorySpread.TimeToNextSpread -= Time.deltaTime;
                if (aggressionMemorySpread.TimeToNextSpread > 0f) continue;

                aggressionMemorySpread.TimeToNextSpread += aggressionMemorySpread.Period;
                var position = GetTransform(i).position;
                var collidersCount =
                    Physics.OverlapSphereNonAlloc(position, aggressionMemorySpread.Distance, _colliders);

                ref readonly var aggressionMemory = ref Read<AggressionMemory>(i);
                if (!aggressionMemory.LastAggressor.IsAlive()) continue;

                for (var colliderIndex = 0; colliderIndex < collidersCount; colliderIndex++)
                {
                    var collider = _colliders[colliderIndex];
                    if (!collider.TryGetComponent(out IEntityView entityView)) continue;
                    if (!entityView.TryGetEntity(out var otherEntity)) continue;
                    if (!otherEntityFilter.Contains(otherEntity, out var otherEntityIdx)) continue;

                    var otherAggressionMemory = Read<AggressionMemory>(otherEntityIdx);
                    if (!otherAggressionMemory.LastAggressor.IsAlive())
                    {
                        ModifyAggressionMemory(otherEntityIdx, aggressionMemory);
                    }
                    else
                    {
                        var priority = _damageTypesPriorityService.GetPriority(aggressionMemory.DamageType);
                        var otherPriority = _damageTypesPriorityService.GetPriority(otherAggressionMemory.DamageType);
                        if (priority > otherPriority)
                            ModifyAggressionMemory(otherEntityIdx, aggressionMemory);
                    }
                }
            }
        }

        private void ModifyAggressionMemory(int otherEntityIdx, in AggressionMemory aggressionMemory)
        {
            Modify<AggressionMemory>(otherEntityIdx) = aggressionMemory;
        }
    }
}