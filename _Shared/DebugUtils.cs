using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace _Shared
{
    public static class DebugUtils
    {
        [Conditional("UNITY_EDITOR")]
        public static void DrawCross(Vector3 position, float radius, Color color, float duration)
        {
            Debug.DrawRay(position, Vector3.up * radius, color, duration);
            Debug.DrawRay(position, Vector3.down * radius, color, duration);
            Debug.DrawRay(position, Vector3.right * radius, color, duration);
            Debug.DrawRay(position, Vector3.left * radius, color, duration);
            Debug.DrawRay(position, Vector3.forward * radius, color, duration);
            Debug.DrawRay(position, Vector3.back * radius, color, duration);
        }
    }
}