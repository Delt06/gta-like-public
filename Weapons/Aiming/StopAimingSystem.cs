using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Aiming
{
    public class StopAimingSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<StopAimingCommand>().Inc<IdleState>().Inc<AimingTag>().End();
            var aimingTags = World.GetObservablePool<AimingTag>();

            foreach (var i in filter)
            {
                aimingTags.Del(i);
            }
        }
    }
}