using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai.RunAway
{
    public class AgentRunAwayDestinationSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilter _aggressorFilter;

        public AgentRunAwayDestinationSystem(EcsWorld world) =>
            _aggressorFilter = world.Filter<UnityRef<Transform>>().End();

        public void Run(EcsSystems systems)
        {
            var filter = Filter<FindDestinationCommand>().Inc<AiRunAwayState>().Inc<AggressionMemory>().IncTransform()
                .End();
            var findDestinationCommands = World.GetReadWritePool<FindDestinationCommand>();
            var aggressionMemories = World.GetReadOnlyPool<AggressionMemory>();
            var transforms = World.GetTransformPool();
            foreach (var i in filter)
            {
                ref var findDestinationCommand = ref findDestinationCommands.Get(i);
                if (findDestinationCommand.FoundDestination != null) continue;

                ref readonly var aggressionMemory = ref aggressionMemories.Read(i);
                var aggressor = aggressionMemory.LastAggressor;
                if (!_aggressorFilter.Contains(aggressor)) continue;

                var aggressorPosition = aggressor.GetTransform().position;
                var position = transforms.Read(i).position;
                const float runDistance = 25f;
                var offset = (position - aggressorPosition).normalized * runDistance;

                findDestinationCommand.FoundDestination = position + offset;
            }
        }
    }
}