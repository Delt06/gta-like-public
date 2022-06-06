using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class StopIfNotIdleSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Exc<IdleState>().End();
            var movementDatas = World.GetReadWritePool<MovementData>();
            foreach (var i in filter)
            {
                movementDatas.Get(i).Direction = Vector3.zero;
            }
        }
    }
}