using System;
using Ai.Attack;
using UnityEngine;

namespace Ai.RunAway
{
    [Serializable]
    public struct PassiveAiData : ITimeToForget
    {
        [Min(0f)]
        public float TimeToForget;
        [Min(0f)]
        public float PathResetPeriod;
        public float GetTimeToForget() => TimeToForget;
    }
}