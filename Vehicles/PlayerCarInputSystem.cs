using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Vehicles
{
    public class PlayerCarInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().Inc<InCarState>().End();
            var inCarStates = World.GetReadWritePool<InCarState>();

            foreach (var i in filter)
            {
                ref var inCarState = ref inCarStates.Get(i);
                inCarState.Throttle = Input.GetAxis("Vertical");
                inCarState.Steering = Input.GetAxis("Horizontal");
                inCarState.IsBraking = Input.GetButton("Jump");
            }
        }
    }
}