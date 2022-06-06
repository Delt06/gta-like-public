using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Active;

namespace Ai.Attack
{
    public class AgentAttackDestinationSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilter _aggressorFilter;

        public AgentAttackDestinationSystem(EcsWorld world) =>
            _aggressorFilter = world.Filter<UnityRef<Transform>>().End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<FindDestinationCommand>()
                         .Inc<AiAttackState>()
                         .Inc<CurrentWeaponInfo>()
                         .Inc<AggressionMemory>()
                         .IncTransform()
                         .End())
            {
                ref var findDestinationCommand = ref Get<FindDestinationCommand>(i);
                if (findDestinationCommand.FoundDestination != null) continue;

                ref readonly var aggressionMemory = ref Read<AggressionMemory>(i);
                var aggressor = aggressionMemory.LastAggressor;
                if (!_aggressorFilter.Contains(aggressor)) continue;

                var aggressorPosition = aggressor.GetTransform().position;
                var position = GetTransform(i).position;
                var desiredDistance = Read<CurrentWeaponInfo>(i).WeaponInfo.DesiredDistance;
                if (Vector3.Distance(aggressorPosition, position) <= desiredDistance) continue;

                findDestinationCommand.FoundDestination = Vector3.MoveTowards(aggressorPosition, position,
                    desiredDistance * 0.5f
                );
            }
        }
    }
}