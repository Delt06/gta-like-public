using Ai._States;
using Movement.Sprint;
using Movement.Walk;

namespace Ai.Attack
{
    public class AgentAttackSprintSystem : AgentStateSprintSystemBase<AiAttackState>
    {
        protected override void UpdateSprint(ref SprintData sprintData, ref WalkData walkData)
        {
            walkData.IsActive = false;
            sprintData.IsActive = false;
        }
    }
}