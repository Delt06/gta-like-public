using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Weapons.Aiming
{
    public class PlayerAimingInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().End();
            var startAimingCommands = World.GetReadWritePool<StartAimingCommand>();
            var stopAimingCommands = World.GetReadWritePool<StopAimingCommand>();
            foreach (var i in filter)
            {
                const int rightMouseButton = 1;
                if (Input.GetMouseButtonDown(rightMouseButton))
                    startAimingCommands.GetOrAdd(i);
                if (Input.GetMouseButtonUp(rightMouseButton))
                    stopAimingCommands.GetOrAdd(i);
            }
        }
    }
}