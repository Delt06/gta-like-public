using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Ai.Idle
{
    public class AgentIdleDestinationSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly WalkingZone _walkingZone;

        public AgentIdleDestinationSystem(WalkingZone walkingZone) => _walkingZone = walkingZone;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<AiIdleState>().Inc<FindDestinationCommand>().End();
            var findDestinationCommands = World.GetReadWritePool<FindDestinationCommand>();
            foreach (var i in filter)
            {
                ref var findDestinationCommand = ref findDestinationCommands.Get(i);
                if (findDestinationCommand.FoundDestination != null) continue;
                if (!_walkingZone.TrySampleRandomPoint(out var destination)) continue;

                findDestinationCommand.FoundDestination = destination;
            }
        }
    }
}