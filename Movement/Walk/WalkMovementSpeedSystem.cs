using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement.Sprint;

namespace Movement.Walk
{
    public class WalkMovementSpeedSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<WalkData>().Exc<EffectiveMovementSpeed>().Inc<SprintAnimationData>().End();
            var walkDatas = World.GetReadOnlyPool<WalkData>();
            var effectiveMovementSpeeds = World.GetReadWritePool<EffectiveMovementSpeed>();
            var sprintAnimationDatas = World.GetReadWritePool<SprintAnimationData>();

            foreach (var i in filter)
            {
                ref readonly var walkData = ref walkDatas.Read(i);
                if (!walkData.IsActive) continue;

                effectiveMovementSpeeds.Add(i).Speed = walkData.Speed;
                sprintAnimationDatas.Get(i).Value = walkData.AnimationValue;
            }
        }
    }
}