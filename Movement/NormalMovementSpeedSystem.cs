using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Movement
{
    public class NormalMovementSpeedSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Exc<EffectiveMovementSpeed>().End();
            var movementDatas = World.GetReadOnlyPool<MovementData>();
            var effectiveMovementSpeeds = World.GetReadWritePool<EffectiveMovementSpeed>();

            foreach (var i in filter)
            {
                effectiveMovementSpeeds.Add(i).Speed = movementDatas.Read(i).NormalSpeed;
            }
        }
    }
}