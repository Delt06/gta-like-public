using System;
using UnityEngine;

namespace Ai.Attack
{
    [Serializable]
    public struct AggressiveAiData : ITimeToForget
    {
        [Min(0f)]
        public float TimeToForget;
        [Min(0f)]
        public float PathResetPeriod;


        public float GetTimeToForget() => TimeToForget;
    }
}