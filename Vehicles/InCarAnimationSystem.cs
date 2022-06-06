using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Vehicles
{
    public class InCarAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsDrivingId = Animator.StringToHash("IsDriving");

        public void Run(EcsSystems systems)
        {
            var filter = Filter<CharacterVehicleData>().Inc<UnityRef<Animator>>().End();
            var animators = World.GetReadOnlyPool<UnityRef<Animator>>();
            var inCarStates = World.GetReadOnlyPool<InCarState>();
            foreach (var i in filter)
            {
                Animator animator = animators.Read(i);
                var isDriving = inCarStates.Has(i);
                animator.SetBool(IsDrivingId, isDriving);
            }
        }
    }
}