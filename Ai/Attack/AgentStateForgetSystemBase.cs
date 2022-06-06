using Ai._States;
using Ai.Idle;
using DELTation.LeoEcsExtensions.Systems.Run;
using Health;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai.Attack
{
    public class AgentStateForgetSystemBase<TState, TData> : EcsSystemBase, IEcsRunSystem
        where TState : struct, IAiState where TData : struct, ITimeToForget
    {
        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<TState>()
                         .Inc<AggressionMemory>()
                         .Inc<TData>()
                         .End())
            {
                ref readonly var aggressionMemory = ref Read<AggressionMemory>(i);
                var aggressiveAiData = Read<TData>(i);
                if (Time.time >= aggressionMemory.LastTime + aggressiveAiData.GetTimeToForget() ||
                    aggressionMemory.LastAggressor.IsDeadOrDestroyed())
                    World.ChangeAiState<TState, AiIdleState>(i);
            }
        }
    }
}