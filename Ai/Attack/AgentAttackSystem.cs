using _Shared.Math;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Active;
using Weapons.Punch;

namespace Ai.Attack
{
    public class AgentAttackSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var aggressorFilter = Filter<UnityRef<Transform>>().End();

            foreach (var i in Filter<AiAttackState>()
                         .Inc<CurrentWeaponInfo>()
                         .Inc<AggressionMemory>()
                         .IncTransform()
                         .Exc<FireCommand>()
                         .End()
                    )
            {
                var aggressor = Read<AggressionMemory>(i).LastAggressor;
                if (!aggressorFilter.Contains(aggressor, out var aggressorIdx)) continue;

                var position = GetTransform(i).position;
                var aggressorPosition = GetTransform(aggressorIdx).position;
                var attackDistance = Read<CurrentWeaponInfo>(i).WeaponInfo.AttackDistance;
                if (VectorUtil.DistanceXz(position, aggressorPosition) > attackDistance) continue;

                Add<FireCommand>(i);
            }
        }
    }
}