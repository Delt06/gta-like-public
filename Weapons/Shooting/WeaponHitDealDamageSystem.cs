using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Health;
using Leopotam.EcsLite;

namespace Weapons.Shooting
{
    public class WeaponHitDealDamageSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilter _damageable;

        public WeaponHitDealDamageSystem(EcsWorld world) =>
            _damageable = world.Filter<HealthData>()
                .End();

        public void Run(EcsSystems systems)
        {
            var filter = Filter<WeaponHitEvent>().End();
            var weaponHitEvents = World.GetReadOnlyPool<WeaponHitEvent>();
            var dealDamageCommands = World.GetReadWritePool<DealDamageCommand>();

            foreach (var i in filter)
            {
                ref readonly var weaponHitEvent = ref weaponHitEvents.Read(i);

                var hitEntity = weaponHitEvent.HitEntity;
                if (!_damageable.Contains(hitEntity)) continue;

                ref var dealDamageCommand = ref dealDamageCommands.Add(World.NewEntity());
                dealDamageCommand.Damage = weaponHitEvent.Damage;
                dealDamageCommand.Entity = hitEntity;
            }
        }
    }
}