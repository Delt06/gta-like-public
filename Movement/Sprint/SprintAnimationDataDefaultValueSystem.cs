using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Movement.Sprint
{
    public class SprintAnimationDataDefaultValueSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<SprintAnimationData>().End();
            var sprintAnimationDatas = World.GetReadWritePool<SprintAnimationData>();

            foreach (var i in filter)
            {
                ref var sprintAnimationData = ref sprintAnimationDatas.Get(i);
                sprintAnimationData.Value = sprintAnimationData.DefaultValue;
            }
        }
    }
}