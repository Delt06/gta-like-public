using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    public class AgentMovementDirectionSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().Inc<MovementData>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            var movementDatas = World.GetReadWritePool<MovementData>();

            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                ref var movementData = ref movementDatas.Get(i);
                var direction = agent.velocity / agent.speed;
                direction = Vector3.ClampMagnitude(direction, 1f);
                movementData.Direction = direction;
            }
        }
    }
}