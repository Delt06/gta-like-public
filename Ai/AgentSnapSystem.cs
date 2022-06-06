using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    public class AgentSnapSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().IncTransform().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            var transforms = World.GetTransformPool();

            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                var agentPosition = agent.nextPosition;
                var actualPosition = transforms.Read(i).position;
                var sqrDifference = (agentPosition - actualPosition).sqrMagnitude;
                const float maxDifference = 0.25f;
                if (sqrDifference > maxDifference * maxDifference) TryWarp(agent, actualPosition);
            }
        }

        private static void TryWarp(NavMeshAgent agent, Vector3 position)
        {
            var velocity = agent.velocity;
            if (agent.Warp(position))
                agent.velocity = velocity;
        }
    }
}