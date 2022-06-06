using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AnimationRigging
{
    [Serializable]
    public struct SpineBones
    {
        [Required]
        public Transform Top;
        public SpineBoneAxes TopAxes;
        [Required]
        public Transform Neck;
        public SpineBoneAxes NeckAxes;
        [Required]
        public Transform Head;
        public SpineBoneAxes HeadAxes;

        public static SpineBones Default => new()
        {
            TopAxes = SpineBoneAxes.Default,
            NeckAxes = SpineBoneAxes.Default,
            HeadAxes = SpineBoneAxes.Default,
        };
    }
}