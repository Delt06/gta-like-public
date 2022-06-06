using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;
using Weapons.Active;

namespace Ai.Attack
{
    public class AgentAttackLookAtSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var aggressorFilter = Filter<UnityRef<Transform>>().End();

            foreach (var i in Filter<AiAttackState>()
                         .Inc<CurrentWeaponInfo>()
                         .Inc<AggressionMemory>()
                         .IncTransform()
                         .Exc<TargetRotationOverride>()
                         .End()
                    )
            {
                var aggressor = Read<AggressionMemory>(i).LastAggressor;
                if (!aggressorFilter.Contains(aggressor, out var aggressorIdx)) continue;

                var position = GetTransform(i).position;
                var aggressorPosition = GetTransform(aggressorIdx).position;
                var offset = aggressorPosition - position;
                offset.y = 0f;
                var lookAtDistance = Read<CurrentWeaponInfo>(i).WeaponInfo.LookAtDistance;
                var offsetMagnitude = offset.magnitude;
                if (offsetMagnitude > lookAtDistance) continue;
                if (offsetMagnitude < 0.1f) continue;

                var direction = offset.normalized;
                Add<TargetRotationOverride>(i).Rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
    }
}