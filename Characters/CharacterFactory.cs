using System.Collections.Generic;
using _Shared;
using AnimationRigging;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Services;
using DELTation.LeoEcsExtensions.Utilities;
using UnityEngine;
using Weapons;
using Weapons.Ik;
using Weapons.Punch;

namespace Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly CharacterFactoryConfig _config;
        private readonly List<CharacterFactoryConfig.NpcSpawnRecord> _npcSpawnRecords = new();
        private readonly IMainEcsWorld _world;

        public CharacterFactory(CharacterFactoryConfig config, IMainEcsWorld world)
        {
            _config = config;
            _world = world;
        }

        public CharacterFacade CreatePlayer(Vector3 position, Quaternion rotation)
        {
            var characterFacade = CreateCharacter(_config.PlayerPrefab, _config.PlayerModelPrefab, position, rotation);
            characterFacade.EntityView.GetOrCreateEntity().GetOrAdd<PlayerTag>();
            return characterFacade;
        }

        public CharacterFacade CreateNpc(Vector3 position, Quaternion rotation)
        {
            var npc = SelectRandomNpc();
            var characterPrefab = npc.CharacterPrefab;
            var modelPrefab = npc.ModelPrefabs[Random.Range(0, npc.ModelPrefabs.Length)];
            return CreateCharacter(characterPrefab, modelPrefab, position, rotation);
        }

        private CharacterFactoryConfig.NpcSpawnRecord SelectRandomNpc()
        {
            _npcSpawnRecords.Clear();
            var weightSum = 0f;

            foreach (var npcSpawnRecord in _config.NpcSpawnRecords)
            {
                _npcSpawnRecords.Add(npcSpawnRecord);
                weightSum += npcSpawnRecord.Weight;
            }

            _npcSpawnRecords.Sort((sr1, sr2) => sr2.Weight.CompareTo(sr1.Weight));

            var cumulativeProbability = 0f;
            var randomValue = Random.value;

            foreach (var npcSpawnRecord in _npcSpawnRecords)
            {
                var probability = npcSpawnRecord.Weight / weightSum;
                cumulativeProbability += probability;
                if (randomValue < cumulativeProbability) return npcSpawnRecord;
            }

            return _npcSpawnRecords[0];
        }

        private CharacterFacade CreateCharacter(CharacterFacade prefab, CharacterModel modelPrefab, Vector3 position,
            Quaternion rotation)
        {
            var characterFacade = Object.Instantiate(prefab, position, rotation);

            var characterModel = Object.Instantiate(modelPrefab, characterFacade.ModelRoot);
            characterModel.Animator.runtimeAnimatorController = _config.AnimatorController;
            characterModel.Animator.applyRootMotion = false;
            characterModel.gameObject.AddComponent<WeaponsAnimationEventBus>();

            var characterModelTransform = characterModel.transform;
            var modelObjectsInfo = Object.Instantiate(_config.ModelObjectsInfoPrefab, characterModelTransform);

            var characterRigsRoot = Object.Instantiate(_config.RigsRootPrefab, characterModelTransform);
            characterRigsRoot.Init(characterModel, modelObjectsInfo);

            characterFacade.EntityView.Construct(_world);
            var entity = characterFacade.EntityView.GetOrCreateEntity();
            entity.Get<ArsenalData>().Weapon = modelObjectsInfo.Pistol;

            ref var characterRigData = ref entity.Add<CharacterRigData>();
            characterRigData.HandIkData = new HandIkData
            {
                Rig = characterRigsRoot.HandsIkRig,
                TargetLeft = modelObjectsInfo.CommonHandleLeft,
                TargetRight = modelObjectsInfo.CommonHandleRight,
            };
            characterRigData.CharacterBonesInfo = characterModel.CharacterBonesInfo;
            characterRigData.AimRig = characterRigsRoot.AimRig;

            entity.Add<UnityRef<Animator>>().Object = characterModel.Animator;

            return characterFacade;
        }
    }
}