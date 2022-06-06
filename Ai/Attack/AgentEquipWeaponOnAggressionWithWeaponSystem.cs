using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Health;
using Leopotam.EcsLite;
using Weapons;

namespace Ai.Attack
{
    public class AgentEquipWeaponOnAggressionWithWeaponSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<AiAttackState>()
                         .IncComponentAndUpdateOf<AggressionMemory>()
                         .Exc<ActiveWeaponData>()
                         .Exc<TakeWeaponCommand>()
                         .End())
            {
                var damageType = Read<AggressionMemory>(i).DamageType;
                if (damageType != DamageType.Weapon) continue;

                Add<TakeWeaponCommand>(i);
            }
        }
    }
}