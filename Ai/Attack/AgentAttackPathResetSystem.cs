using _Shared.States;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ai.Attack
{
    public class AgentAttackPathResetSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<IdleState>().Inc<AiAttackState>().Inc<AggressiveAiData>().Inc<UnityRef<NavMeshAgent>>()
                .End();
            var aiAttackStates = World.GetReadWritePool<AiAttackState>();
            var aggressiveAiDatas = World.GetReadOnlyPool<AggressiveAiData>();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            foreach (var i in filter)
            {
                ref var aiAttackState = ref aiAttackStates.Get(i);
                aiAttackState.ResetTimer += Time.deltaTime;
                ref readonly var aggressiveAiData = ref aggressiveAiDatas.Read(i);
                if (aiAttackState.ResetTimer < aggressiveAiData.PathResetPeriod) continue;

                aiAttackState.ResetTimer = 0f;
                NavMeshAgent agent = agents.Read(i);
                agent.ResetPath();
            }
        }
    }
}