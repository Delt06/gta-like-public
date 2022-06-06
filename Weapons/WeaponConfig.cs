using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] [InlineProperty] [HideLabel]
        private WeaponInfo _weaponInfo;

        public WeaponInfo WeaponInfo => _weaponInfo;
    }
}