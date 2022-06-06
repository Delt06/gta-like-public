using _Shared;
using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class PlayerMovementInputSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public PlayerMovementInputSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().Inc<IdleState>().Inc<MovementData>().End();
            var movementDatas = World.GetReadWritePool<MovementData>();

            foreach (var i in filter)
            {
                var camera = _sceneData.Camera.transform;

                var x = Input.GetAxis("Horizontal");
                var z = Input.GetAxis("Vertical");
                var cameraForward = RemoveYAndNormalize(camera.forward);
                var cameraRight = RemoveYAndNormalize(camera.right);
                var direction = cameraForward * z + cameraRight * x;
                direction = Vector3.ClampMagnitude(direction, 1f);

                ref var movementData = ref movementDatas.Get(i);
                movementData.Direction = direction;
            }
        }

        private static Vector3 RemoveYAndNormalize(Vector3 vector)
        {
            vector.y = 0f;
            return vector.normalized;
        }
    }
}