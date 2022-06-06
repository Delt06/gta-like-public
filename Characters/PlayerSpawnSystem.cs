using _Shared;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Characters
{
    public class PlayerSpawnSystem : EcsSystemBase, IEcsInitSystem
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly RuntimeData _runtimeData;
        private readonly SceneData _sceneData;

        public PlayerSpawnSystem(SceneData sceneData, RuntimeData runtimeData,
            ICharacterFactory characterFactory)
        {
            _sceneData = sceneData;
            _runtimeData = runtimeData;
            _characterFactory = characterFactory;
        }

        public void Init(EcsSystems systems)
        {
            var playerSpawnPoint = _sceneData.PlayerSpawnPoint;
            var player = _characterFactory.CreatePlayer(playerSpawnPoint.position, playerSpawnPoint.rotation);
            _runtimeData.Player = player.EntityView;
        }
    }
}