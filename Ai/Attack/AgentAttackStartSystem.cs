using Ai._States;
using Ai.Idle;
using Leopotam.EcsLite;

namespace Ai.Attack
{
    public class AgentAttackStartSystem : AiTransitionOnAggressionChangeSystemBase<AiIdleState, AiAttackState>
    {
        protected override void ConfigureFilter(EcsWorld.Mask filter) =>
            filter.Inc<AggressiveAiData>();
    }
}