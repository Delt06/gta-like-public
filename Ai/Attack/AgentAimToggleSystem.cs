using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine.AI;
using Weapons;
using Weapons.Aiming;

namespace Ai.Attack
{
    public class AgentAimToggleSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<UnityRef<NavMeshAgent>>()
                         .Inc<AiAttackState>()
                         .Inc<ActiveWeaponData>()
                         .Exc<AimingTag>()
                         .Exc<StartAimingCommand>()
                         .End())
            {
                Add<StartAimingCommand>(i);
            }

            foreach (var i in Filter<UnityRef<NavMeshAgent>>()
                         .Exc<AiAttackState>()
                         .Inc<AimingTag>()
                         .Exc<StopAimingCommand>()
                         .End())
            {
                Add<StopAimingCommand>(i);
            }
        }
    }
}