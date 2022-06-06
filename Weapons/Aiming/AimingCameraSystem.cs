using _Shared;
using _Shared.UI;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Weapons.Aiming
{
    public class AimingCameraSystem : EcsSystemBase, IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData;
        private readonly UiRoot _uiRoot;

        public AimingCameraSystem(SceneData sceneData, UiRoot uiRoot)
        {
            _sceneData = sceneData;
            _uiRoot = uiRoot;
        }

        public void Init(EcsSystems systems)
        {
            ToggleCrosshairImage(false);
        }

        public void Run(EcsSystems systems)
        {
            var filter = Filter<PlayerTag>().IncUpdateOf<AimingTag>().End();
            var aimingTags = World.GetReadOnlyPool<AimingTag>();

            foreach (var i in filter)
            {
                var aimVirtualCamera = _sceneData.AimVirtualCamera;
                var aiming = aimingTags.Has(i);
                aimVirtualCamera.enabled = aiming;
                ToggleCrosshairImage(aiming);
            }
        }

        private void ToggleCrosshairImage(bool enabled)
        {
            _uiRoot.CrosshairImage.enabled = enabled;
        }
    }
}