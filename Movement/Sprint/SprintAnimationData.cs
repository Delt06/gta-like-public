using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement.Sprint
{
    [Serializable]
    public struct SprintAnimationData
    {
        [Min(0f)]
        public float AnimationDampTime;
        [Min(0f)]
        public float DefaultValue;

        [HideInEditorMode]
        public float Value;
    }
}