using Ai._States;
using Movement.Sprint;
using Movement.Walk;

namespace Ai.Idle
{
    public class AgentIdleSprintSystem : AgentStateSprintSystemBase<AiIdleState>
    {
        protected override void UpdateSprint(ref SprintData sprintData, ref WalkData walkData)
        {
            sprintData.IsActive = false;
            walkData.IsActive = true;
        }
    }
}