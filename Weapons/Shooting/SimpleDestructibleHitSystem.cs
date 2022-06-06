using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Weapons.Shooting
{
    public class SimpleDestructibleHitSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilter _simpleDestructiblesFilter;

        public SimpleDestructibleHitSystem(EcsWorld world) =>
            _simpleDestructiblesFilter = world.Filter<SimpleDestructibleTag>()
                .Inc<ViewBackRef>()
                .End();

        public void Run(EcsSystems systems)
        {
            var filter = Filter<WeaponHitEvent>().End();
            var weaponHitEvents = World.GetReadOnlyPool<WeaponHitEvent>();

            foreach (var i in filter)
            {
                var hitEntity = weaponHitEvents.Read(i).HitEntity;
                if (!_simpleDestructiblesFilter.Contains(hitEntity)) continue;

                hitEntity.GetView().Destroy();
            }
        }
    }
}