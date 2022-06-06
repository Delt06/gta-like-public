using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Health
{
    public class HealthViewLookAtCameraSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public HealthViewLookAtCameraSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<HealthViewData>().End();
            var healthViewDatas = World.GetReadOnlyPool<HealthViewData>();
            var cameraForward = _sceneData.Camera.transform.forward;

            foreach (var i in filter)
            {
                ref readonly var healthViewData = ref healthViewDatas.Read(i);
                healthViewData.RootTransform.forward = cameraForward;
            }
        }
    }
}