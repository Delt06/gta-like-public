using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;

namespace Ai
{
    // https://answers.unity.com/questions/1649578/navmeshagent-getting-stuck-on-a-random-point.html
    // Was noticed when an agent gets hit by a car
    public class AgentStuckWorkaroundSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();

            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                if (!agent.enabled) continue;
                if (agent.hasPath) continue;
                if (agent.pathStatus != NavMeshPathStatus.PathComplete) continue;

                agent.enabled = false;
                // ReSharper disable once Unity.InefficientPropertyAccess
                agent.enabled = true;
            }
        }
    }
}