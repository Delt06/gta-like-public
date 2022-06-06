using Sirenix.OdinInspector;
using UnityEngine;

namespace Vehicles
{
    public class CarSeat : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _exitPosition;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Transform GetTransform() => _transform;

        public Transform GetExitPosition() => _exitPosition;
    }
}