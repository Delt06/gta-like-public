using DELTation.LeoEcsExtensions.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Shared
{
    public static class ColliderExtensions
    {
        public static bool TryGetEntityInRigidbodyOrCollider(this Collider collider,
            out EcsPackedEntityWithWorld entity)
        {
            var attachedRigidbody = collider.attachedRigidbody;
            if (attachedRigidbody != null &&
                TryGetEntity(collider, out entity))
                return true;

            return TryGetEntity(collider, out entity);
        }

        private static bool TryGetEntity(Component component, out EcsPackedEntityWithWorld entity)
        {
            entity = default;
            return component.TryGetComponent(out IEntityView entityView) &&
                   entityView.TryGetEntity(out entity);
        }
    }
}