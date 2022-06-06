using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Vehicles
{
    public class CarCameraSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        public CarCameraSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().IncUpdateOf<InCarState>().End();
            var inCarStates = World.GetReadOnlyPool<InCarState>();

            foreach (var i in filter)
            {
                var camera = _sceneData.CarVirtualCamera;

                var inCar = inCarStates.Has(i);
                if (inCar)
                {
                    var car = inCarStates.Read(i).Car;
                    var carTransform = car.Rigidbody.transform;
                    camera.Follow = camera.LookAt = carTransform;
                    camera.enabled = true;
                }
                else
                {
                    camera.Follow = camera.LookAt = null;
                    camera.enabled = false;
                }
            }
        }
    }
}