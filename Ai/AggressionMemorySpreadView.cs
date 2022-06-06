using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai
{
    public class AggressionMemorySpreadView : ComponentView<AggressionMemorySpread>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            ref var aggressionMemorySpread = ref entity.Get<AggressionMemorySpread>();
            aggressionMemorySpread.TimeToNextSpread = Random.Range(0f, aggressionMemorySpread.Period);
        }
    }
}