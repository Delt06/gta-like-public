using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ai
{
    [Serializable]
    public struct AggressionMemorySpread
    {
        [Min(0f)]
        public float Distance;
        [Min(0f)]
        public float Period;
        [HideInEditorMode]
        public float TimeToNextSpread;
    }
}