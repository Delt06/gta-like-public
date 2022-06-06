using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Health
{
    public class HealthDeathSystem<TState> : EcsSystemBase, IEcsRunSystem where TState : struct, IState
    {
        [UsedImplicitly]
        public HealthDeathSystem() { }

        public void Run(EcsSystems systems)
        {
            var filter = FilterAndIncUpdateOf<HealthData>().Inc<TState>().End();
            var healthDatas = World.GetReadOnlyPool<HealthData>();
            foreach (var i in filter)
            {
                var health = healthDatas.Read(i).Health;
                if (health > 0f) continue;

                filter.ChangeState<TState, DeadState>(i);
            }
        }
    }
}