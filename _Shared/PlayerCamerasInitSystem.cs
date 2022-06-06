using System;
using Cinemachine;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Shared
{
    public class PlayerCamerasInitSystem : EcsSystemBase, IEcsInitSystem
    {
        private readonly RuntimeData _runtimeData;
        private readonly SceneData _sceneData;

        public PlayerCamerasInitSystem(RuntimeData runtimeData, SceneData sceneData)
        {
            _runtimeData = runtimeData;
            _sceneData = sceneData;
        }

        public void Init(EcsSystems systems)
        {
            var player = _runtimeData.Player;

            if (player == null) throw new InvalidOperationException("Player has not been created yet.");

            var playerTransform = player.GetOrCreateEntity().GetTransform();
            AssignFollowAndLookAt(_sceneData.MainVirtualCamera, playerTransform);
            AssignFollowAndLookAt(_sceneData.AimVirtualCamera, playerTransform);
        }

        private static void AssignFollowAndLookAt(ICinemachineCamera virtualCamera, Transform transform)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }
}