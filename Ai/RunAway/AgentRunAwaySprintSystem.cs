using Ai._States;
using Movement.Sprint;
using Movement.Walk;

namespace Ai.RunAway
{
    public class AgentRunAwaySprintSystem : AgentStateSprintSystemBase<AiRunAwayState>
    {
        protected override void UpdateSprint(ref SprintData sprintData, ref WalkData walkData)
        {
            sprintData.IsActive = true;
            walkData.IsActive = false;
        }
    }
}