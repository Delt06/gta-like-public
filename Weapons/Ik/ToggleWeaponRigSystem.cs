using AnimationRigging;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Weapons.Ik
{
    public class ToggleWeaponRigSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<CharacterRigData>().IncUpdateOf<ActiveWeaponData>().End();
            var characterRigDatas = World.GetReadOnlyPool<CharacterRigData>();
            var activeWeaponDatas = World.GetReadOnlyPool<ActiveWeaponData>();

            foreach (var i in filter)
            {
                var rig = characterRigDatas.Read(i).HandIkData.Rig;
                var enableRig = activeWeaponDatas.Has(i);
                rig.weight = enableRig ? 1 : 0f;
            }
        }
    }
}