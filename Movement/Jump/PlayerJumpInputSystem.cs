using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement.Jump
{
    public class PlayerJumpInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            if (!Input.GetButton("Jump")) return;

            var filter = Filter<PlayerTag>().Inc<JumpData>().Exc<JumpCommand>().End();
            var jumpCommands = World.GetReadWritePool<JumpCommand>();

            foreach (var i in filter)
            {
                jumpCommands.Add(i);
            }
        }
    }
}