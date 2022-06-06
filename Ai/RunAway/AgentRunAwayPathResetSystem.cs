using _Shared.States;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ai.RunAway
{
    public class AgentRunAwayPathResetSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<IdleState>().Inc<AiRunAwayState>().Inc<PassiveAiData>().Inc<UnityRef<NavMeshAgent>>()
                .End();
            var aiRunAwayStates = World.GetReadWritePool<AiRunAwayState>();
            var passiveAiDatas = World.GetReadOnlyPool<PassiveAiData>();
            var agents = World.GetReadOnlyPool<UnityRef<NavMeshAgent>>();
            foreach (var i in filter)
            {
                ref var aiRunAwayState = ref aiRunAwayStates.Get(i);
                aiRunAwayState.ResetTimer += Time.deltaTime;
                ref readonly var passiveAiData = ref passiveAiDatas.Read(i);
                if (aiRunAwayState.ResetTimer < passiveAiData.PathResetPeriod) continue;

                aiRunAwayState.ResetTimer = 0f;
                NavMeshAgent agent = agents.Read(i);
                agent.ResetPath();
            }
        }
    }
}