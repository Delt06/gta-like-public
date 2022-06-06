using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health
{
    public static class EntityUtils
    {
        public static bool IsDeadOrDestroyed(this EcsPackedEntityWithWorld entity) =>
            !entity.IsAlive() || entity.Has<DeadState>();
    }
}