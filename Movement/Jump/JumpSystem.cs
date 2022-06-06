using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement.Jump
{
    public class JumpSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<JumpCommand>().Inc<IdleState>().Inc<MovementData>().Inc<JumpData>().End();

            var movementDatas = World.GetReadWritePool<MovementData>();
            var jumpDatas = World.GetReadOnlyPool<JumpData>();


            foreach (var i in filter)
            {
                ref var movementData = ref movementDatas.Get(i);
                if (!movementData.IsGrounded) continue;

                ref readonly var jumpData = ref jumpDatas.Read(i);
                var jumpSpeed = JumpHeightToSpeed(jumpData.JumpHeight);
                movementData.ExtraVelocity.y = jumpSpeed;
            }
        }

        private static float JumpHeightToSpeed(float height)
        {
            var gravityMagnitude = Physics.gravity.magnitude;
            return Mathf.Sqrt(2 * gravityMagnitude * height);
        }
    }
}