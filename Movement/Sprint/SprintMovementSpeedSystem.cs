using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Movement.Sprint
{
    public class SprintMovementSpeedSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<SprintData>().Inc<SprintAnimationData>().Exc<EffectiveMovementSpeed>().End();
            var sprintDatas = World.GetReadOnlyPool<SprintData>();
            var effectiveMovementSpeeds = World.GetReadWritePool<EffectiveMovementSpeed>();
            var sprintAnimationDatas = World.GetReadWritePool<SprintAnimationData>();

            foreach (var i in filter)
            {
                ref readonly var sprintData = ref sprintDatas.Read(i);
                if (!sprintData.IsActive) continue;

                effectiveMovementSpeeds.Add(i).Speed = sprintData.Speed;
                sprintAnimationDatas.Get(i).Value = sprintData.AnimationValue;
            }
        }
    }
}