using Sirenix.OdinInspector;
using UnityEngine;

namespace Vehicles
{
    public class Car : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _centerOfMass;

        [SerializeField] [Required] [AssetSelector]
        private CarConfig _carConfig;
        [SerializeField] [Required] [ChildGameObjectsOnly]
        private CarSeat _driverSeat;
        [SerializeField] [Required] [ChildGameObjectsOnly]
        private Rigidbody _rigidbody;
        [SerializeField] [Required] [ChildGameObjectsOnly]
        private CarWheel[] _wheels;

        public CarConfig CarConfig => _carConfig;
        public CarSeat DriverSeat => _driverSeat;
        public Rigidbody Rigidbody => _rigidbody;

        public CarWheel[] Wheels => _wheels;

        private void Awake()
        {
            _rigidbody.centerOfMass = _rigidbody.transform.InverseTransformPoint(_centerOfMass.position);
        }
    }
}