using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement.Sprint
{
    public class PlayerSprintInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().Inc<SprintData>().End();
            var sprintDatas = World.GetReadWritePool<SprintData>();
            foreach (var i in filter)
            {
                ref var sprintData = ref sprintDatas.Get(i);
                sprintData.IsActive = Input.GetKey(KeyCode.LeftShift);
            }
        }
    }
}