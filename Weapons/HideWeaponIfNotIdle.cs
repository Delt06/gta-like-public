using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons
{
    public class HideWeaponIfNotIdle : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<ActiveWeaponData>().Exc<IdleState>().End();
            var hideWeaponCommands = World.GetReadWritePool<HideWeaponCommand>();

            foreach (var i in filter)
            {
                hideWeaponCommands.GetOrAdd(i);
            }
        }
    }
}