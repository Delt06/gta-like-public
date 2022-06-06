using System;
using UnityEngine;

namespace Movement.Sprint
{
    [Serializable]
    public struct SprintData
    {
        [Min(0f)]
        public float Speed;
        public bool IsActive;
        [Min(0f)]
        public float AnimationValue;
    }
}