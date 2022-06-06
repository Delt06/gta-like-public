using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Ai
{
    public class WalkingZone : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _maxSamplingDistance = 5f;
        [SerializeField] [Min(0f)] private float _maxAreaCost = 5f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Center(), Scale());
        }

        public bool TrySampleRandomPoint(out Vector3 point)
        {
            var center = Center();
            var scale = Scale();
            var halfScale = scale * 0.5f;
            var randomValue = RandomVector3();
            var offset = math.lerp(-halfScale, halfScale, randomValue);
            point = center + offset;
            if (!NavMesh.SamplePosition(point, out var hit, _maxSamplingDistance, NavMesh.AllAreas))
                return false;

            var cost = NavMeshUtils.GetMaxAreaCost(hit.mask);

            if (cost > _maxAreaCost)
                return false;

            point = hit.position;
            return true;
        }

        private static float3 RandomVector3() =>
            new float3(
                Random.value, Random.value, Random.value
            );

        private float3 Center() => transform.position;

        private float3 Scale() => transform.lossyScale;
    }
}