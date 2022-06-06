using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    [Serializable]
    public struct ArsenalData
    {
        [Required]
        public Weapon Weapon;
        [Min(0f)]
        public float PunchDamage;
    }
}