using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Weapons.Punch
{
    public class PlayerFireInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var filter = Filter<PlayerTag>().End();
            var fireCommands = World.GetReadWritePool<FireCommand>();

            foreach (var i in filter)
            {
                fireCommands.GetOrAdd(i);
            }
        }
    }
}