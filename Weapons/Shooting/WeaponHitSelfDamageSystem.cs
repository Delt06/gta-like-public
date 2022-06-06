using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Shooting
{
    public class WeaponHitSelfDamageSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<WeaponHitEvent>().End())
            {
                ref readonly var weaponHitEvent = ref Read<WeaponHitEvent>(i);
                if (!weaponHitEvent.HitEntity.EqualsTo(weaponHitEvent.Hitter)) continue;

                Del<WeaponHitEvent>(i);
            }
        }
    }
}