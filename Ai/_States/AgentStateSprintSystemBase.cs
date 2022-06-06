using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement.Sprint;
using Movement.Walk;

namespace Ai._States
{
    public abstract class AgentStateSprintSystemBase<TState> : EcsSystemBase, IEcsRunSystem
        where TState : struct, IAiState
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<SprintData>().Inc<WalkData>().Inc<TState>().End();
            var sprintDatas = World.GetReadWritePool<SprintData>();
            var walkDatas = World.GetReadWritePool<WalkData>();

            foreach (var i in filter)
            {
                UpdateSprint(ref sprintDatas.Get(i), ref walkDatas.Get(i));
            }
        }

        protected abstract void UpdateSprint(ref SprintData sprintData, ref WalkData walkData);
    }
}