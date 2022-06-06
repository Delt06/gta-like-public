using AnimationRigging;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Weapons.Aiming
{
    public class AimRigSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = FilterOnUpdateOf<AimingTag>().Inc<CharacterRigData>().End();
            var aimingTags = World.GetReadOnlyPool<AimingTag>();
            var aimRigDatas = World.GetReadOnlyPool<CharacterRigData>();

            foreach (var i in filter)
            {
                var aiming = aimingTags.Has(i);
                var rig = aimRigDatas.Read(i).AimRig;
                rig.weight = aiming ? 1f : 0f;
            }
        }
    }
}