using System;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct MovementAnimationData
    {
        [Min(0f)]
        public float DirectionThreshold;
        [Min(0f)]
        public float MinMovementSpeed;
    }
}