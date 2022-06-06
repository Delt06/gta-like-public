using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AnimationRigging
{
    [Serializable]
    public struct ArmBones
    {
        [Required]
        public Transform Arm;
        [Required]
        public Transform ForeArm;
        [Required]
        public Transform Hand;
        public Vector3 HandleBasePosition;
        public Quaternion HandleBaseRotation;
    }
}