using System;
using UnityEngine;

namespace Weapons
{
    [Serializable]
    public struct WeaponInfo
    {
        [Min(0f)]
        public float DesiredDistance;
        [Min(0f)]
        public float LookAtDistance;
        [Min(0f)]
        public float AttackDistance;
        public Vector3 AimLocalPosition;
    }
}