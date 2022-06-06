using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;

namespace Ai
{
    public class AgentSetDestinationSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().Inc<FindDestinationCommand>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            var findDestinationCommands = World.GetReadOnlyPool<FindDestinationCommand>();
            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                var destination = findDestinationCommands.Read(i).FoundDestination;
                if (destination == null) continue;

                agent.SetDestination(destination.Value);
            }
        }
    }
}