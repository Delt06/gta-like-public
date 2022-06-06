using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Ai._States
{
    public class AiTransitionOnAggressionChangeSystemBase<TFrom, TTo> : EcsSystemBase, IEcsRunSystem
        where TFrom : struct, IAiState where TTo : struct, IAiState
    {
        public void Run(EcsSystems systems)
        {
            var mask = FilterAndIncUpdateOf<AggressionMemory>().Inc<TFrom>();
            ConfigureFilter(mask);
            var filter = mask.End();
            foreach (var i in filter)
            {
                World.ChangeAiState<TFrom, TTo>(i);
            }
        }

        protected virtual void ConfigureFilter(EcsWorld.Mask filter) { }
    }
}