using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health
{
    [CreateAssetMenu]
    public class DamageTypesConfig : ScriptableObject
    {
        [SerializeField] private int _fallbackPriority;
        [SerializeField] [TableList] private DamageTypePriority[] _damageTypePriorities;

        public int FallbackPriority => _fallbackPriority;
        public DamageTypePriority[] DamageTypePriorities => _damageTypePriorities;


        [Serializable]
        public struct DamageTypePriority
        {
            public DamageType DamageType;
            [Min(0)]
            public int Priority;
        }
    }
}