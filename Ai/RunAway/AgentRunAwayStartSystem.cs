using Ai._States;
using Ai.Idle;
using Leopotam.EcsLite;

namespace Ai.RunAway
{
    public class AgentRunAwayStartSystem : AiTransitionOnAggressionChangeSystemBase<AiIdleState, AiRunAwayState>
    {
        protected override void ConfigureFilter(EcsWorld.Mask filter) =>
            filter.Inc<PassiveAiData>();
    }
}