using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement;

namespace Health.Ragdoll
{
    public class DeathPoseSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<DeadState>().Exc<PoseLock>().End();
            var poseLocks = World.GetReadWritePool<PoseLock>();
            foreach (var i in filter)
            {
                poseLocks.Add(i);
            }
        }
    }
}