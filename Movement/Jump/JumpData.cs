using System;
using UnityEngine;

namespace Movement.Jump
{
    [Serializable]
    public struct JumpData
    {
        [Min(0f)]
        public float JumpHeight;
    }
}