using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement;

namespace Vehicles
{
    public class InCarMovementPoseSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<InCarState>().Exc<PoseLock>().End();
            var poseLocks = World.GetReadWritePool<PoseLock>();

            foreach (var i in filter)
            {
                poseLocks.Add(i);
            }
        }
    }
}