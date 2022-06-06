using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu]
    public class WeaponsStaticData : ScriptableObject
    {
        [SerializeField] [Required] private ParticleSystem _weaponTrailPrefab;
        [SerializeField] [Min(0f)] private float _punchRadius = 0.25f;
        [SerializeField] [Required] [AssetSelector]
        private WeaponConfig _fistsConfig;

        public ParticleSystem WeaponTrailPrefab => _weaponTrailPrefab;

        public float PunchRadius => _punchRadius;

        public WeaponConfig FistsConfig => _fistsConfig;
    }
}