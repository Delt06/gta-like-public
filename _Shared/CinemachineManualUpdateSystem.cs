using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace _Shared
{
    public class CinemachineManualUpdateSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public CinemachineManualUpdateSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            _sceneData.CinemachineBrain.ManualUpdate();
        }
    }
}