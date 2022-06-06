using System;
using Health;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;

namespace Ai
{
    [Serializable]
    public struct AggressionMemory
    {
        [HideInEditorMode]
        public float LastTime;
        [HideInEditorMode]
        public DamageType DamageType;
        [HideInEditorMode]
        public EcsPackedEntityWithWorld LastAggressor;
    }
}