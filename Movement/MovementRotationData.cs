using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct MovementRotationData
    {
        [Min(0f)]
        public float DirectionThreshold;
        [Min(0f)]
        public float SmoothTime;
        [Min(0f)]
        public float OverrideSmoothTime;

        [HideInEditorMode]
        public Quaternion Velocity;
        [HideInEditorMode]
        public Vector3 LastSignificantDirection;
    }
}