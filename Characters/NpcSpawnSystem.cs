using _Shared;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Characters
{
    public class NpcSpawnSystem : EcsSystemBase, IEcsInitSystem
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly SceneData _sceneData;

        public NpcSpawnSystem(SceneData sceneData, ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
            _sceneData = sceneData;
        }

        public void Init(EcsSystems systems)
        {
            foreach (Transform npcSpawnPointRoot in _sceneData.NpcSpawnPointsRoot)
            {
                _characterFactory.CreateNpc(npcSpawnPointRoot.position, npcSpawnPointRoot.rotation);
            }
        }
    }
}