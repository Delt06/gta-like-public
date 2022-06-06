using System;
using UnityEngine.Animations.Rigging;

namespace AnimationRigging
{
    [Serializable]
    public struct SpineBoneAxes
    {
        public MultiAimConstraintData.Axis AimAxis;
        public MultiAimConstraintData.Axis UpAxis;

        public static SpineBoneAxes Default => new()
        {
            AimAxis = MultiAimConstraintData.Axis.Z,
            UpAxis = MultiAimConstraintData.Axis.Y,
        };
    }
}