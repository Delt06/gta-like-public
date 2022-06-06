using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Movement.Sprint;

namespace Weapons
{
    public class WeaponSprintSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<ActiveWeaponData>().Inc<SprintData>().End();
            var sprintDatas = World.GetReadWritePool<SprintData>();

            foreach (var i in filter)
            {
                sprintDatas.Get(i).IsActive = false;
            }
        }
    }
}