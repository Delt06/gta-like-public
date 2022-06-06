using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Health;
using Leopotam.EcsLite;
using UnityEngine.AI;

namespace Ai
{
    public class DeathAgentDisableSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().IncComponentAndUpdateOf<DeadState>().End();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            foreach (var i in filter)
            {
                agents.Read(i).Object.enabled = false;
            }
        }
    }
}