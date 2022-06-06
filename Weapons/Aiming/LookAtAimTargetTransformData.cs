using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons.Aiming
{
    [Serializable]
    public struct LookAtAimTargetTransformData
    {
        [Required] public Transform Transform;
    }
}