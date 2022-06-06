using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Aiming
{
    public class PlayerAimTargetCameraSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public PlayerAimTargetCameraSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<LookAtAimTargetTransformData>().Inc<PlayerTag>().End();
            var cameraLookAtTransformDatas = World.GetReadOnlyPool<LookAtAimTargetTransformData>();

            foreach (var i in filter)
            {
                var transform = cameraLookAtTransformDatas.Read(i).Transform;
                var cameraLookAt = _sceneData.CameraLookAt;
                transform.SetPositionAndRotation(cameraLookAt.position, cameraLookAt.rotation);
            }
        }
    }
}