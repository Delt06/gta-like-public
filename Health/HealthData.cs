using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health
{
    [Serializable]
    public struct HealthData
    {
        [Min(0f)]
        public float MaxHealth;
        [HideInEditorMode]
        public float Health;
    }
}