using _Shared.States;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;

namespace Ai
{
    public class AgentStartFindingDestinationSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<IdleState>().Inc<UnityRef<NavMeshAgent>>().Exc<FindDestinationCommand>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            var findDestinationCommands = World.GetReadWritePool<FindDestinationCommand>();
            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                if (agent.hasPath || agent.pathPending) continue;

                findDestinationCommands.Add(i);
            }
        }
    }
}