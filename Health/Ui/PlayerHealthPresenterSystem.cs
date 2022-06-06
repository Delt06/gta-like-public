using _Shared;
using _Shared.UI;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health.Ui
{
    public class PlayerHealthPresenterSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly HealthBar _playerHealthBar;

        public PlayerHealthPresenterSystem(UiRoot uiRoot) => _playerHealthBar = uiRoot.PlayerHealthBar;

        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter<PlayerTag>().IncComponentAndUpdateOf<HealthData>().End())
            {
                var healthData = Read<HealthData>(i);
                _playerHealthBar.SetFill(healthData.Health / healthData.MaxHealth);
            }
        }
    }
}