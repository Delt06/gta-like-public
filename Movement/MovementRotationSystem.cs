using _Shared.Math;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementRotationSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Exc<PoseLock>().Inc<MovementRotationData>().End();
            var movementDatas = World.GetReadOnlyPool<MovementData>();
            var rotationDatas = World.GetReadWritePool<MovementRotationData>();
            var targetRotationOverrides = World.GetReadOnlyPool<TargetRotationOverride>();
            foreach (var i in filter)
            {
                ref readonly var movementData = ref movementDatas.Read(i);
                ref var movementRotationData = ref rotationDatas.Get(i);

                var direction = movementData.Direction;
                Quaternion targetRotation;
                float smoothTime;
                if (targetRotationOverrides.Has(i))
                {
                    targetRotation = targetRotationOverrides.Read(i).Rotation;
                    smoothTime = movementRotationData.OverrideSmoothTime;
                }
                else
                {
                    if (direction.magnitude >= movementRotationData.DirectionThreshold)
                        movementRotationData.LastSignificantDirection = direction;

                    var lastSignificantDirection = movementRotationData.LastSignificantDirection;
                    if (lastSignificantDirection.magnitude < movementRotationData.DirectionThreshold)
                        continue;

                    var forward = lastSignificantDirection;
                    forward.y = 0f;
                    targetRotation = Quaternion.LookRotation(forward, Vector3.up);
                    smoothTime = movementRotationData.SmoothTime;
                }

                var transform = movementData.CharacterController.transform;

                transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation, targetRotation,
                    ref movementRotationData.Velocity, smoothTime, Time.deltaTime
                );
            }
        }
    }
}