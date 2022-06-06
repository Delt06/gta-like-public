using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct MovementData
    {
        [Required]
        public CharacterController CharacterController;
        [Min(0f)]
        public float NormalSpeed;

        [HideInEditorMode]
        public float LastSpeed;
        [HideInEditorMode]
        public Vector3 Direction;
        [HideInEditorMode]
        public Vector3 ExtraVelocity;
        [HideInEditorMode]
        public bool IsGrounded;
    }
}