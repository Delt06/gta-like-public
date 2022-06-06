using UnityEngine;

namespace _Shared.Math
{
    public static class VectorUtil
    {
        public static float DistanceXz(Vector3 p0, Vector3 p1)
        {
            var offset = p0 - p1;
            offset.y = 0f;
            return offset.magnitude;
        }
    }
}