using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Active
{
    public class UpdateCurrentWeaponInfo : EcsSystemBase, IEcsRunSystem
    {
        private readonly WeaponConfig _fistsConfig;

        public UpdateCurrentWeaponInfo(WeaponsStaticData weaponsStaticData) =>
            _fistsConfig = weaponsStaticData.FistsConfig;

        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<ArsenalData>().Exc<CurrentWeaponInfo>().End())
            {
                var weaponInfo = Has<ActiveWeaponData>(i)
                    ? Read<ActiveWeaponData>(i).Weapon.Config.WeaponInfo
                    : _fistsConfig.WeaponInfo;
                Add<CurrentWeaponInfo>(i).WeaponInfo = weaponInfo;
            }
        }
    }
}