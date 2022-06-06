using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    public static class NavMeshUtils
    {
        public static float GetMaxAreaCost(int areaMask)
        {
            var maxCost = float.NegativeInfinity;

            for (var areaIndex = 0; areaIndex < sizeof(int); areaIndex++)
            {
                if (!AreaMaskContainsArea(areaMask, areaIndex)) continue;

                var cost = NavMesh.GetAreaCost(areaIndex);
                maxCost = Mathf.Max(maxCost, cost);
            }

            return maxCost;
        }

        public static bool AreaMaskContainsArea(int areaMask, int areaIndex) =>
            (areaMask & (1 << areaIndex)) != 0;
    }
}