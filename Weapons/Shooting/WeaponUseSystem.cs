using _Shared;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Health;
using Leopotam.EcsLite;
using UnityEngine;
using Weapons.Aiming;
using Weapons.Punch;

namespace Weapons.Shooting
{
    public class WeaponUseSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly SceneData _sceneData;
        private readonly WeaponTrailFactory _trailFactory;

        public WeaponUseSystem(SceneData sceneData, WeaponTrailFactory trailFactory)
        {
            _sceneData = sceneData;
            _trailFactory = trailFactory;
        }

        private Camera Camera => _sceneData.Camera;

        public void Run(EcsSystems systems)
        {
            var filter = Filter<FireCommand>().Inc<ActiveWeaponData>().End();
            var activeWeaponDatas = World.GetReadOnlyPool<ActiveWeaponData>();
            var weaponHitEvents = World.GetReadWritePool<WeaponHitEvent>();
            var aimingTags = World.GetReadOnlyPool<AimingTag>();
            foreach (var i in filter)
            {
                var weapon = activeWeaponDatas.Read(i).Weapon;
                if (!weapon.TryUse()) continue;

                var shootFrom = weapon.ShootFrom;
                var ray = aimingTags.Has(i) ? GetCameraCenterRay() : new Ray(shootFrom.position, shootFrom.forward);

                ray.origin += ray.direction * GetViewportZ(shootFrom.position);

                var maxDistance = weapon.MaxDistance;
                if (!Physics.Raycast(ray, out var hit, maxDistance, LayerMaskUtil.All,
                        QueryTriggerInteraction.Ignore
                    ))
                {
                    CreateTrail(shootFrom, maxDistance);
                    continue;
                }

                CreateTrail(shootFrom, hit.distance, (hit.point - shootFrom.position).normalized);

                DebugUtils.DrawCross(hit.point, 0.25f, Color.red, 0.5f);

                if (!hit.collider.TryGetEntityInRigidbodyOrCollider(out var hitEntity))
                    continue;

                ref var weaponHitEvent = ref weaponHitEvents.Add(World.NewEntity());
                weaponHitEvent.HitEntity = hitEntity;
                weaponHitEvent.Damage = weapon.Damage;
                weaponHitEvent.Hitter = World.PackEntityWithWorld(i);
                weaponHitEvent.DamageType = DamageType.Weapon;
            }
        }

        private void CreateTrail(Transform shootFrom, float distance, Vector3? direction = null)
        {
            var ray = new Ray(shootFrom.position, direction ?? shootFrom.forward);
            _trailFactory.Create(ray, distance);
        }

        private float GetViewportZ(Vector3 shootFromPosition) =>
            Camera.WorldToViewportPoint(shootFromPosition).z;

        private Ray GetCameraCenterRay() =>
            Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }
}