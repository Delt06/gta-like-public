using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Health
{
    public class HealthDataView : UpdatableComponentView<HealthData>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            ref var healthData = ref entity.Get<HealthData>();
            healthData.Health = healthData.MaxHealth;
        }
    }
}