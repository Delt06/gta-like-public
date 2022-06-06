using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Weapons.Punch
{
    public class PunchAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsPunchingId = Animator.StringToHash("IsPunching");
        private static readonly int PunchIndexId = Animator.StringToHash("PunchIndex");

        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<Animator>>().IncUpdateOf<PunchingState>().End();
            var animators = World.GetReadOnlyPool<UnityRef<Animator>>();
            var punchingStates = World.GetReadOnlyPool<PunchingState>();

            foreach (var i in filter)
            {
                Animator animator = animators.Read(i);
                var isPunching = punchingStates.Has(i);
                animator.SetBool(IsPunchingId, isPunching);
                animator.SetInteger(PunchIndexId, Random.Range(0, 2));
            }
        }
    }
}