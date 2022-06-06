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
    public class CarEnterSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly Collider[] _results = new Collider[16];

        public void Run(EcsSystems systems)
        {
            var filter = Filter<CarEnterCommand>().Inc<IdleState>().Inc<MovementData>().Inc<CharacterVehicleData>()
                .IncTransform().End();
            var movementDatas = World.GetReadOnlyPool<MovementData>();
            var carVehicleDatas = World.GetReadOnlyPool<CharacterVehicleData>();
            var transforms = World.GetTransformPool();

            foreach (var i in filter)
            {
                if (!movementDatas.Read(i).IsGrounded) continue;

                ref readonly var carVehicleData = ref carVehicleDatas.Read(i);
                var transform = transforms.Read(i);

                var center = transform.position;
                var radius = carVehicleData.EnterMaxDistance;
                var resultsCount = Physics.OverlapSphereNonAlloc(center, radius, _results,
                    LayerMaskUtil.All,
                    QueryTriggerInteraction.Ignore
                );

                for (var resultIndex = 0; resultIndex < resultsCount; resultIndex++)
                {
                    var collider = _results[resultIndex];
                    if (!collider.attachedRigidbody) continue;
                    if (!collider.attachedRigidbody.TryGetComponent(out Car car)) continue;

                    EnterCar(transform, car);
                    filter.ChangeState<IdleState, InCarState>(i).Car = car;
                    break;
                }
            }
        }

        private static void EnterCar(Transform transform, Car car)
        {
            var seatTransform = car.DriverSeat.GetTransform();
            transform.SetParent(seatTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}