using Leopotam.EcsLite;

namespace Health
{
    public struct DealDamageCommand
    {
        public EcsPackedEntityWithWorld Entity;
        public float Damage;
    }
}