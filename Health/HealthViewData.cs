using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health
{
    [Serializable]
    public struct HealthViewData
    {
        [Required]
        public SpriteRenderer Sprite;
        [Required]
        public Transform RootTransform;
        public Gradient HealthGradient;
        [Range(0f, 1f)]
        public float MaxRatioToDisplay;
    }
}