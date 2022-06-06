using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    public class NavMeshAgentRefView : ComponentView<UnityRef<NavMeshAgent>>
    {
        [SerializeField] private bool _autoUpdate;

        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            NavMeshAgent navMeshAgent = entity.Get<UnityRef<NavMeshAgent>>();
            navMeshAgent.updatePosition = _autoUpdate;
            navMeshAgent.updateRotation = _autoUpdate;
            navMeshAgent.updateUpAxis = _autoUpdate;
        }
    }
}