using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Health
{
    public class HealthViewUpdateSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = FilterAndIncUpdateOf<HealthData>().Inc<HealthViewData>().End();
            var healthDatas = World.GetReadOnlyPool<HealthData>();
            var healthViewDatas = World.GetReadOnlyPool<HealthViewData>();

            foreach (var i in filter)
            {
                ref readonly var healthData = ref healthDatas.Read(i);
                ref readonly var healthViewData = ref healthViewDatas.Read(i);

                var ratio = healthData.Health / healthData.MaxHealth;
                var sprite = healthViewData.Sprite;
                if (ratio > healthViewData.MaxRatioToDisplay)
                {
                    sprite.enabled = false;
                }
                else
                {
                    sprite.color = healthViewData.HealthGradient.Evaluate(Mathf.Clamp01(ratio));
                    sprite.enabled = true;
                }
            }
        }
    }
}