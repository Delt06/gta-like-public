using Sirenix.OdinInspector;
using UnityEngine;

namespace Vehicles
{
    public class CarWheel : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _visualTransform;
        [SerializeField] [Required] private Collider _collider;
        [SerializeField] private bool _steer;
        [SerializeField] [Min(0f)] private float _radius;
        [SerializeField] [Min(0f)] private float _suspensionSmoothTime = 0.5f;

        private Vector3 _initialVisualsLocalPosition;
        private float _suspensionDistance;
        private float _suspensionDistanceVelocity;
        private float _targetSuspensionDistance;
        private Vector3 _visualLocalRotation;

        public bool Steer => _steer;

        public Collider Collider => _collider;

        public float Radius => _radius;

        public Vector3 VisualLocalRotation
        {
            get => _visualLocalRotation;
            set
            {
                _visualLocalRotation = value;
                _visualTransform.localEulerAngles = _visualLocalRotation;
            }
        }

        private void Awake()
        {
            _initialVisualsLocalPosition = _visualTransform.localPosition;
        }

        private void Update()
        {
            _suspensionDistance = Mathf.SmoothDamp(_suspensionDistance, _targetSuspensionDistance,
                ref _suspensionDistanceVelocity, _suspensionSmoothTime, float.PositiveInfinity, Time.deltaTime
            );
            _visualTransform.localPosition = _initialVisualsLocalPosition + Vector3.down * _suspensionDistance;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetTransform().position, _radius);
        }

        public void SetSuspensionDistance(float distance)
        {
            _targetSuspensionDistance = Mathf.Max(0f, distance - _radius);
        }

        public Transform GetTransform() => transform;
    }
}