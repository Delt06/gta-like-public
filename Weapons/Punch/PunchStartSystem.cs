using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement;

namespace Weapons.Punch
{
    public class PunchStartSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<FireCommand>().Inc<IdleState>().Exc<ActiveWeaponData>().Inc<MovementData>().End();
            var movementDatas = World.GetReadOnlyPool<MovementData>();

            foreach (var i in filter)
            {
                if (!movementDatas.Read(i).IsGrounded) continue;

                filter.ChangeState<IdleState, PunchingState>(i);
            }
        }
    }
}