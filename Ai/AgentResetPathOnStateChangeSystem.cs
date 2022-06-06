using Ai._States;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;

namespace Ai
{
    public class AgentResetPathOnStateChangeSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<AiStateChangedEvent>().Inc<UnityRef<NavMeshAgent>>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                if (agent.isOnNavMesh)
                    agent.ResetPath();
            }
        }
    }
}