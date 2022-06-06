using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Weapons
{
    public class ToggleWeaponInputSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().Inc<ArsenalData>().End();
            var takeWeaponCommands = World.GetReadWritePool<TakeWeaponCommand>();
            var hideWeaponCommands = World.GetReadWritePool<HideWeaponCommand>();
            var activeWeaponDatas = World.GetReadOnlyPool<ActiveWeaponData>();

            foreach (var i in filter)
            {
                if (!Input.GetKeyDown(KeyCode.T)) continue;

                if (activeWeaponDatas.Has(i))
                    hideWeaponCommands.GetOrAdd(i);
                else
                    takeWeaponCommands.GetOrAdd(i);
            }
        }
    }
}