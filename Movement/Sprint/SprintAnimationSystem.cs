using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement.Sprint
{
    public class SprintAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int SprintId = Animator.StringToHash("Sprint");

        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<Animator>>().Inc<SprintAnimationData>().End();
            var animators = World.GetReadOnlyPool<UnityRef<Animator>>();
            var sprintAnimationDatas = World.GetReadOnlyPool<SprintAnimationData>();
            foreach (var i in filter)
            {
                Animator animator = animators.Read(i);
                var sprintAnimationData = sprintAnimationDatas.Read(i);
                var sprintValue = sprintAnimationData.Value;
                animator.SetFloat(SprintId, sprintValue, sprintAnimationData.AnimationDampTime, Time.deltaTime);
            }
        }
    }
}