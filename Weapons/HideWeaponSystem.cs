using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons
{
    public class HideWeaponSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<HideWeaponCommand>().Inc<ActiveWeaponData>().End();
            var activeWeaponDatas = World.GetObservablePool<ActiveWeaponData>();
            foreach (var i in filter)
            {
                var weapon = activeWeaponDatas.Read(i).Weapon;
                activeWeaponDatas.Del(i);
                weapon.SetActive(false);
            }
        }
    }
}