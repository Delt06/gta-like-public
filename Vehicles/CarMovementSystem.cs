using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;

namespace Vehicles
{
    public class CarMovementSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<InCarState>().End();
            var inCarStates = World.GetReadOnlyPool<InCarState>();

            foreach (var i in filter)
            {
                ref readonly var inCarState = ref inCarStates.Read(i);
                var car = inCarState.Car;
                var carConfig = car.CarConfig;
                var rigidbody = car.Rigidbody;
                var carTransform = rigidbody.transform;
                var forward = carTransform.forward;

                var down = -carTransform.up;
                ProcessSuspension(car.Wheels, carConfig, down, out var ratioOfGroundedWheels);

                if (inCarState.IsBraking)
                {
                    SetPhysicMaterialForAllWheels(car, carConfig.BrakePhysicMaterial);
                }
                else
                {
                    var throttle = inCarState.Throttle;
                    var throttleAcceleration = forward * carConfig.EnginePower * throttle;
                    if (throttle < 0f)
                        throttleAcceleration *= carConfig.ReverseThrottleMultiplier;
                    throttleAcceleration *= ratioOfGroundedWheels;
                    rigidbody.AddForce(throttleAcceleration * Time.deltaTime, ForceMode.VelocityChange);
                    SetPhysicMaterialForAllWheels(car, carConfig.NormalPhysicMaterial);
                }

                var steeringVelocity = carTransform.up *
                                       (Vector3.Dot(forward, rigidbody.velocity) *
                                        carConfig.LinearToSteeringVelocity *
                                        inCarState.Steering);
                steeringVelocity = Vector3.ClampMagnitude(steeringVelocity, carConfig.MaxSteeringSpeed);
                steeringVelocity *= ratioOfGroundedWheels;
                var steeringDelta = steeringVelocity * Time.deltaTime;
                var newRotation = Quaternion.Euler(steeringDelta) * rigidbody.rotation;
                rigidbody.MoveRotation(newRotation);

                if (rigidbody.velocity.magnitude > carConfig.MinSpeedForGroundAcceleration)
                {
                    var groundAcceleration = down * carConfig.GroundAcceleration * ratioOfGroundedWheels;
                    rigidbody.AddForce(groundAcceleration, ForceMode.Acceleration);
                }

                UpdateVisuals(carConfig, inCarState, car);
            }
        }

        private static void ProcessSuspension(CarWheel[] carWheels, CarConfig carConfig, Vector3 carDown,
            out float groundedWheelsRatio)
        {
            var groundedCount = 0;
            var upDot = Vector3.Dot(-carDown, Vector3.up);
            var useSuspension = upDot >= carConfig.SuspensionMinUpDot;

            foreach (var carWheel in carWheels)
            {
                var maxSuspensionDistance = carWheel.Radius + carConfig.WheelContactCheckExtraDistance;
                float suspensionDistance;
                if (IsGrounded(carWheel, maxSuspensionDistance, carDown, out var distance))
                {
                    carWheel.SetSuspensionDistance(distance);
                    suspensionDistance = distance;
                    groundedCount++;
                }
                else
                {
                    suspensionDistance = maxSuspensionDistance;
                }

                if (!useSuspension)
                    suspensionDistance = 0f;

                carWheel.SetSuspensionDistance(suspensionDistance);
            }

            groundedWheelsRatio = (float) groundedCount / carWheels.Length;
        }

        private static bool IsGrounded(CarWheel carWheel, float maxSuspensionDistance, Vector3 carDown,
            out float distance)
        {
            var transform = carWheel.GetTransform();
            var origin = transform.position;
            if (Physics.Raycast(origin, carDown, out var hit, maxSuspensionDistance, LayerMaskUtil.All,
                    QueryTriggerInteraction.Ignore
                ))
            {
                distance = hit.distance;
                return true;
            }

            distance = default;
            return false;
        }

        private static void SetPhysicMaterialForAllWheels(Car car, [CanBeNull] PhysicMaterial physicMaterial)
        {
            foreach (var wheel in car.Wheels)
            {
                wheel.Collider.sharedMaterial = physicMaterial;
            }
        }

        private static void UpdateVisuals(CarConfig carConfig, in InCarState inCarState, Car car)
        {
            var steeringAngle = carConfig.MaxSteeringAngle * inCarState.Steering;
            var throttleAngularVelocity = inCarState.Throttle * carConfig.ThrottleToAngularVelocity;
            var deltaX = throttleAngularVelocity * Time.deltaTime;

            foreach (var wheel in car.Wheels)
            {
                var localRotation = wheel.VisualLocalRotation;
                localRotation.x += deltaX;

                if (wheel.Steer)
                    localRotation.y = steeringAngle;

                wheel.VisualLocalRotation = localRotation;
            }
        }
    }
}