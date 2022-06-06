using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Weapons.Aiming
{
    public class AimingTargetRotationOverrideSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public AimingTargetRotationOverrideSystem(SceneData sceneData) => _sceneData = sceneData;


        public void Run(EcsSystems systems)
        {
            var filter = Filter<AimingTag>()
                .Inc<PlayerTag>()
                .IncTransform()
                .Inc<MovementRotationData>()
                .Exc<TargetRotationOverride>()
                .End();
            var transforms = World.GetTransformPool();
            var targetRotationOverrides = World.GetReadWritePool<TargetRotationOverride>();
            var movementDatas = World.GetReadWritePool<MovementRotationData>();

            foreach (var i in filter)
            {
                var position = transforms.Read(i).position;
                var towardsLookAt = _sceneData.CameraLookAt.position - position;
                towardsLookAt.y = 0f;
                towardsLookAt.Normalize();
                var rotation = Quaternion.LookRotation(towardsLookAt, Vector3.up);
                targetRotationOverrides.Add(i).Rotation = rotation;

                movementDatas.Get(i).LastSignificantDirection = rotation * Vector3.forward;
            }
        }
    }
}