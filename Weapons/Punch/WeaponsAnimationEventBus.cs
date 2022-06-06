using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using JetBrains.Annotations;
using UnityEngine;

namespace Weapons.Punch
{
    public class WeaponsAnimationEventBus : MonoBehaviour
    {
        private IEntityView _entityView;

        private void Awake()
        {
            _entityView = GetComponentInParent<IEntityView>();
        }

        [UsedImplicitly]
        public void OnFinishedPunch()
        {
            if (_entityView.TryGetEntity(out var entity))
                entity.GetOrAdd<FinishedPunchEvent>();
        }

        [UsedImplicitly]
        public void OnDealtDamage(int handIndex)
        {
            if (_entityView.TryGetEntity(out var entity))
                entity.GetOrAdd<DealPunchDamageCommand>().HandType = IndexToHand(handIndex);
        }

        private static Hand IndexToHand(int handIndex) => handIndex == 0 ? Hand.Right : Hand.Left;
    }
}