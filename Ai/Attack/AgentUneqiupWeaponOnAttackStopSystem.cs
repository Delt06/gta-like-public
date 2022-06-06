using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;
using Weapons;

namespace Ai.Attack
{
    public class AgentUneqiupWeaponOnAttackStopSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<ActiveWeaponData>()
                         .Inc<UnityRef<NavMeshAgent>>()
                         .Exc<AiAttackState>()
                         .Exc<HideWeaponCommand>()
                         .End())
            {
                Add<HideWeaponCommand>(i);
            }
        }
    }
}