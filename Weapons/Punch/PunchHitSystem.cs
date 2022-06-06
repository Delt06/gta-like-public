using System;
using _Shared;
using AnimationRigging;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Health;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Shooting;

namespace Weapons.Punch
{
    public class PunchHitSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly Collider[] _hits = new Collider[16];
        private readonly WeaponsStaticData _weaponsStaticData;

        public PunchHitSystem(WeaponsStaticData weaponsStaticData) => _weaponsStaticData = weaponsStaticData;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<CharacterRigData>().Inc<DealPunchDamageCommand>().Inc<ArsenalData>().End();
            var characterRigDatas = World.GetReadOnlyPool<CharacterRigData>();
            var dealPunchDamageCommands = World.GetReadOnlyPool<DealPunchDamageCommand>();
            var arsenalDatas = World.GetReadOnlyPool<ArsenalData>();
            var weaponHitEvents = World.GetReadWritePool<WeaponHitEvent>();

            foreach (var i in filter)
            {
                var characterBonesInfo = characterRigDatas.Read(i).CharacterBonesInfo;
                var handType = dealPunchDamageCommands.Read(i).HandType;
                var hand = handType switch
                {
                    Hand.Left => characterBonesInfo.LeftArm.Hand,
                    Hand.Right => characterBonesInfo.RightArm.Hand,
                    _ => throw new ArgumentOutOfRangeException(),
                };

                var position = hand.position;
                var punchRadius = _weaponsStaticData.PunchRadius;
                var hitsCount = Physics.OverlapSphereNonAlloc(position, punchRadius, _hits,
                    LayerMaskUtil.All, QueryTriggerInteraction.Ignore
                );
                DebugUtils.DrawCross(position, punchRadius, Color.red, 0.5f);

                for (var hitIndex = 0; hitIndex < hitsCount; hitIndex++)
                {
                    var hit = _hits[hitIndex];
                    if (!hit.TryGetEntityInRigidbodyOrCollider(out var hitEntity)) continue;

                    ref var weaponHitEvent = ref weaponHitEvents.Add(World.NewEntity());
                    weaponHitEvent.HitEntity = hitEntity;
                    weaponHitEvent.Damage = arsenalDatas.Read(i).PunchDamage;
                    weaponHitEvent.Hitter = World.PackEntityWithWorld(i);
                    weaponHitEvent.DamageType = DamageType.Melee;
                }
            }
        }
    }
}