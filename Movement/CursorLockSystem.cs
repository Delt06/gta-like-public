using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class CursorLockSystem : EcsSystemBase, IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}