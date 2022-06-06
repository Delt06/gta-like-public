using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement;
using UnityEngine.AI;

namespace Ai
{
    public class AgentEffectiveMovementSpeedSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<UnityRef<NavMeshAgent>>().Inc<EffectiveMovementSpeed>().End();
            var agents = World.GetPool<UnityRef<NavMeshAgent>>().AsReadOnly();
            var effectiveMovementSpeeds =
                World.GetPool<EffectiveMovementSpeed>().AsReadOnly();

            foreach (var i in filter)
            {
                NavMeshAgent agent = agents.Read(i);
                agent.speed = effectiveMovementSpeeds.Read(i).Speed;
            }
        }
    }
}