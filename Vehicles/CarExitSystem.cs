using _Shared;
using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Vehicles
{
    public class CarExitSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<CarExitCommand>().Inc<InCarState>().IncTransform().Inc<MovementData>().End();
            var inCarStates = World.GetReadOnlyPool<InCarState>();
            var transforms = World.GetTransformPool();
            var movementDatas = World.GetReadOnlyPool<MovementData>();

            foreach (var i in filter)
            {
                ref readonly var inCarState = ref inCarStates.Read(i);
                var characterController = movementDatas.Read(i).CharacterController;
                var car = inCarState.Car;
                var exitPosition = car.DriverSeat.GetExitPosition();

                var characterControllerCenter = characterController.center;
                var radius = characterController.radius;
                var halfHeight = characterController.height * 0.5f;
                var capsuleStart =
                    exitPosition.TransformPoint(characterControllerCenter +
                                                Vector3.down * (halfHeight - radius)
                    );
                var capsuleEnd =
                    exitPosition.TransformPoint(characterControllerCenter +
                                                Vector3.up * (halfHeight - radius)
                    );
                if (Physics.CheckCapsule(capsuleStart, capsuleEnd, radius, LayerMaskUtil.All,
                        QueryTriggerInteraction.Ignore
                    )) continue;

                var transform = transforms.Read(i);
                ExitCar(transform, exitPosition);
                filter.ChangeState<InCarState, IdleState>(i);
            }
        }

        private static void ExitCar(Transform transform, Transform exitPosition)
        {
            transform.SetParent(null);
            transform.localPosition = exitPosition.position;
            transform.localRotation = exitPosition.rotation;
        }
    }
}