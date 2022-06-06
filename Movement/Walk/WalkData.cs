using System;
using UnityEngine;

namespace Movement.Walk
{
    [Serializable]
    public struct WalkData
    {
        [Min(0f)]
        public float Speed;
        public bool IsActive;
        [Min(0f)]
        public float AnimationValue;
    }
}