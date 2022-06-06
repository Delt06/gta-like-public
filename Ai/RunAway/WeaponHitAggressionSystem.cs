using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Shooting;

namespace Ai.RunAway
{
    public class WeaponHitAggressionSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var hitEntityFilter = Filter<AggressionMemory>().End();

            foreach (var i in Filter<WeaponHitEvent>().End())
            {
                ref readonly var weaponHitEvent = ref Read<WeaponHitEvent>(i);
                var hitEntity = weaponHitEvent.HitEntity;
                if (!hitEntityFilter.Contains(hitEntity, out var hitEntityIdx)) continue;

                ref var aggressionMemory = ref Modify<AggressionMemory>(hitEntityIdx);
                aggressionMemory.LastAggressor = weaponHitEvent.Hitter;
                aggressionMemory.LastTime = Time.time;
                aggressionMemory.DamageType = weaponHitEvent.DamageType;
            }
        }
    }
}