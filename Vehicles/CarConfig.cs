using Sirenix.OdinInspector;
using UnityEngine;

namespace Vehicles
{
    [CreateAssetMenu]
    public class CarConfig : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _enginePower;
        [SerializeField] [Min(0f)] private float _linearToSteeringVelocity;
        [SerializeField] [Min(0f)] private float _maxSteeringSpeed;
        [SerializeField] [Required] private PhysicMaterial _normalPhysicMaterial;
        [SerializeField] [Required] private PhysicMaterial _brakePhysicMaterial;
        [SerializeField] [Min(0f)] private float _reverseThrottleMultiplier;
        [SerializeField] [Min(0f)] private float _wheelContactCheckExtraDistance = 0.1f;
        [SerializeField] [Min(0f)] private float _minSpeedForGroundAcceleration;
        [SerializeField] [Min(0f)] private float _groundAcceleration;
        [SerializeField] [Min(0f)] private float _maxSteeringAngle;
        [SerializeField] [Min(0f)] private float _throttleToAngularVelocity;
        [SerializeField] [Min(0f)] private float _suspensionMinUpDot = 0.75f;

        public float EnginePower => _enginePower;

        public float LinearToSteeringVelocity => _linearToSteeringVelocity;

        public float MaxSteeringSpeed => _maxSteeringSpeed;

        public PhysicMaterial NormalPhysicMaterial => _normalPhysicMaterial;

        public PhysicMaterial BrakePhysicMaterial => _brakePhysicMaterial;

        public float ReverseThrottleMultiplier => _reverseThrottleMultiplier;

        public float WheelContactCheckExtraDistance => _wheelContactCheckExtraDistance;

        public float MinSpeedForGroundAcceleration => _minSpeedForGroundAcceleration;

        public float GroundAcceleration => _groundAcceleration;

        public float MaxSteeringAngle => _maxSteeringAngle;

        public float ThrottleToAngularVelocity => _throttleToAngularVelocity;

        public float SuspensionMinUpDot => _suspensionMinUpDot;
    }
}