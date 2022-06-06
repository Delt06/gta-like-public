using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health
{
    public class DealDamageSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilter _entityFilter;

        public DealDamageSystem(EcsWorld world) => _entityFilter = world.Filter<HealthData>().End();


        public void Run(EcsSystems systems)
        {
            var filter = Filter<DealDamageCommand>().End();
            var dealDamageCommands = World.GetReadOnlyPool<DealDamageCommand>();

            foreach (var i in filter)
            {
                ref readonly var dealDamageCommand = ref dealDamageCommands.Read(i);
                if (!_entityFilter.Contains(dealDamageCommand.Entity)) continue;

                dealDamageCommand.Entity.Modify<HealthData>().Health -= dealDamageCommand.Damage;
            }
        }
    }
}