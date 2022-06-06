using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Health
{
    public class DeathAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsDeadId = Animator.StringToHash("IsDead");

        public void Run(EcsSystems systems)
        {
            var filter = FilterAndIncUpdateOf<DeadState>().Inc<UnityRef<Animator>>().End();
            var animators = World.GetReadOnlyPool<UnityRef<Animator>>();

            foreach (var i in filter)
            {
                Animator animator = animators.Read(i);
                animator.SetBool(IsDeadId, true);
            }
        }
    }
}