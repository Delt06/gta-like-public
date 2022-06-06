using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementGravitySystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Exc<PoseLock>().End();
            var movementDatas = World.GetReadWritePool<MovementData>();

            foreach (var i in filter)
            {
                ref var movementData = ref movementDatas.Get(i);
                movementData.ExtraVelocity += Physics.gravity * Time.deltaTime;
                var gravityMotion = movementData.ExtraVelocity * Time.deltaTime;
                movementData.CharacterController.Move(gravityMotion);
                var isGrounded = movementData.CharacterController.isGrounded;
                if (isGrounded) movementData.ExtraVelocity.y = 0f;

                movementData.IsGrounded = isGrounded;
            }
        }
    }
}