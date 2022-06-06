using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons
{
    public class TakeWeaponSystem : EcsSystemBase, IEcsInitSystem, IEcsRunSystem
    {
        public void Init(EcsSystems systems)
        {
            var filter = Filter<ArsenalData>().End();
            var arsenalDatas = World.GetReadOnlyPool<ArsenalData>();

            foreach (var i in filter)
            {
                arsenalDatas.Read(i).Weapon.SetActive(false);
            }
        }

        public void Run(EcsSystems systems)
        {
            var filter = Filter<TakeWeaponCommand>().Inc<IdleState>().Inc<ArsenalData>().Exc<ActiveWeaponData>().End();
            var arsenalDatas = World.GetReadOnlyPool<ArsenalData>();
            var activeWeaponDatas = World.GetObservablePool<ActiveWeaponData>();

            foreach (var i in filter)
            {
                var weapon = arsenalDatas.Read(i).Weapon;
                activeWeaponDatas.Add(i).Weapon = weapon;
                weapon.SetActive(true);
            }
        }
    }
}