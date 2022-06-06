using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Aiming
{
    public class StartAimingSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<StartAimingCommand>().Inc<IdleState>().Exc<AimingTag>().End();
            var aimingTags = World.GetObservablePool<AimingTag>();

            foreach (var i in filter)
            {
                aimingTags.Add(i);
            }
        }
    }
}