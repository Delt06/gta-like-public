using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Active;
using Weapons.Aiming;

namespace Ai.Attack
{
    public class AgentAimTargetAggressorSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var aggressorFilter = Filter<UnityRef<Transform>>().End();
            var filter = Filter<AiAttackState>().Inc<CurrentWeaponInfo>().Inc<AimingTag>().Inc<AggressionMemory>()
                .End();
            foreach (var i in filter)
            {
                var lastAggressor = Read<AggressionMemory>(i).LastAggressor;
                if (!aggressorFilter.Contains(lastAggressor, out var lastAggressorIdx)) continue;

                var weaponInfo = Read<CurrentWeaponInfo>(i).WeaponInfo;

                var transform = Read<LookAtAimTargetTransformData>(i).Transform;
                var aggressorAimPoint = GetTransform(lastAggressorIdx).TransformPoint(weaponInfo.AimLocalPosition);
                transform.position = aggressorAimPoint;
                transform.localRotation = Quaternion.identity;
            }
        }
    }
}