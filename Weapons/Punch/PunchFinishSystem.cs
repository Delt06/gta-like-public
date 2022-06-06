using _Shared.States;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Punch
{
    public class PunchFinishSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<FinishedPunchEvent>().Inc<PunchingState>().End();

            foreach (var i in filter)
            {
                filter.ChangeState<PunchingState, IdleState>(i);
            }
        }
    }
}