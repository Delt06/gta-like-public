using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsMovingId = Animator.StringToHash("IsMoving");
        private static readonly int MovementSpeedId = Animator.StringToHash("MovementSpeed");
        private static readonly int IsGroundedId = Animator.StringToHash("IsGrounded");

        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Inc<MovementAnimationData>().Inc<UnityRef<Animator>>().End();
            var movementDatas = World.GetReadOnlyPool<MovementData>();
            var movementAnimationDatas = World.GetReadOnlyPool<MovementAnimationData>();
            var animators = World.GetReadOnlyPool<UnityRef<Animator>>();

            foreach (var i in filter)
            {
                ref readonly var movementData = ref movementDatas.Read(i);
                ref readonly var movementAnimationData = ref movementAnimationDatas.Read(i);
                Animator animator = animators.Read(i);

                var direction = movementData.Direction;
                var directionMagnitude = direction.magnitude;

                var isMoving = directionMagnitude >= movementAnimationData.DirectionThreshold;
                animator.SetBool(IsMovingId, isMoving);

                var movementSpeed = directionMagnitude * movementData.LastSpeed;
                movementSpeed = Mathf.Max(movementSpeed, movementAnimationData.MinMovementSpeed);
                animator.SetFloat(MovementSpeedId, movementSpeed);

                animator.SetBool(IsGroundedId, movementData.IsGrounded);
            }
        }
    }
}