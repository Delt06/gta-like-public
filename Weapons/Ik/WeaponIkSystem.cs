using AnimationRigging;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Weapons.Ik
{
    public class WeaponIkSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<CharacterRigData>().Inc<ActiveWeaponData>().End();
            var characterRigDatas = World.GetReadOnlyPool<CharacterRigData>();
            var activeWeaponDatas = World.GetReadOnlyPool<ActiveWeaponData>();

            foreach (var i in filter)
            {
                ref readonly var activeWeaponData = ref activeWeaponDatas.Read(i);
                ref readonly var characterRigData = ref characterRigDatas.Read(i);
                ref readonly var ikData = ref characterRigData.HandIkData;
                var characterBonesInfo = characterRigData.CharacterBonesInfo;

                var weapon = activeWeaponData.Weapon;
                Transfer(weapon.HandleRight, ikData.TargetRight, characterBonesInfo.RightArm);
                Transfer(weapon.HandleLeft, ikData.TargetLeft, characterBonesInfo.LeftArm);
            }
        }

        private static void Transfer(Transform from, Transform to, in ArmBones armBones)
        {
            to.position = from.TransformPoint(armBones.HandleBasePosition);
            to.rotation = from.rotation * armBones.HandleBaseRotation;
        }
    }
}