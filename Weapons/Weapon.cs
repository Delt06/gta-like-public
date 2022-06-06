using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _handleRight;
        [SerializeField] [Required] private Transform _handleLeft;
        [SerializeField] [Required] private Transform _shootFrom;
        [SerializeField] [Min(0f)] private float _maxDistance = 25f;

        [SerializeField] [Required] private Transform _root;
        [SerializeField] private Vector3 _recoilOffset = Vector3.back;
        [SerializeField] private Quaternion _recoilRotationOffset = Quaternion.identity;
        [SerializeField] [Min(0f)] private float _recoilDuration = 0.5f;
        [SerializeField] [Min(0f)] private float _recoilRestoreDuration = 0.5f;
        [SerializeField] [Min(0f)] private float _damage = 25f;
        [SerializeField] [Min(0f)] private float _cooldown = 0.25f;
        [SerializeField] [Required] private WeaponConfig _config;

        private float _lastShotTime = float.NegativeInfinity;

        private Vector3 _rootRestLocalPosition;
        private Quaternion _rootRestLocalRotation;

        public WeaponConfig Config => _config;

        public Transform HandleRight => _handleRight;

        public Transform HandleLeft => _handleLeft;

        public Transform ShootFrom => _shootFrom;

        public float MaxDistance => _maxDistance;

        public float Damage => _damage;

        private void Awake()
        {
            _rootRestLocalPosition = _root.localPosition;
            _rootRestLocalRotation = _root.localRotation;
        }

        private void OnDestroy()
        {
            this.DOKill();
        }

        public bool TryUse()
        {
            var now = Time.time;
            if (now < _lastShotTime + _cooldown) return false;
            _lastShotTime = now;
            this.DOKill();
            DOTween.Sequence().SetId(this)
                .Append(_root.DOLocalMove(_rootRestLocalPosition + _recoilOffset, _recoilDuration))
                .Join(_root.DOLocalRotateQuaternion(_recoilRotationOffset * _rootRestLocalRotation, _recoilDuration))
                .Append(_root.DOLocalMove(_rootRestLocalPosition, _recoilRestoreDuration))
                .Join(_root.DOLocalRotateQuaternion(_rootRestLocalRotation, _recoilRestoreDuration))
                ;
            return true;
        }

        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}