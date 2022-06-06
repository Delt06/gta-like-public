using Health;
using Leopotam.EcsLite;

namespace Weapons.Shooting
{
    public struct WeaponHitEvent
    {
        public EcsPackedEntityWithWorld HitEntity;
        public EcsPackedEntityWithWorld Hitter;
        public float Damage;
        public DamageType DamageType;
    }
}