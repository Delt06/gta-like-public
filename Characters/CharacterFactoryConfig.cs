using System;
using _Shared.Attributes;
using AnimationRigging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu]
    public class CharacterFactoryConfig : ScriptableObject
    {
        [SerializeField] [PrefabAssetSelector]
        private CharacterFacade _playerPrefab;
        [SerializeField] [PrefabAssetSelector]
        private CharacterModel _playerModelPrefab;
        [SerializeField] [TableList] private NpcSpawnRecord[] _npcSpawnRecords;
        [SerializeField] [PrefabAssetSelector]
        private ModelObjectsInfo _modelObjectsInfoPrefab;
        [SerializeField] [PrefabAssetSelector]
        private CharacterRigsRoot _rigsRootPrefab;
        [SerializeField] [Required]
        private RuntimeAnimatorController _animatorController;

        public CharacterFacade PlayerPrefab => _playerPrefab;

        public CharacterModel PlayerModelPrefab => _playerModelPrefab;

        public NpcSpawnRecord[] NpcSpawnRecords => _npcSpawnRecords;

        public RuntimeAnimatorController AnimatorController => _animatorController;


        public ModelObjectsInfo ModelObjectsInfoPrefab => _modelObjectsInfoPrefab;

        public CharacterRigsRoot RigsRootPrefab => _rigsRootPrefab;

        [Serializable]
        public struct NpcSpawnRecord
        {
            [PrefabAssetSelector] public CharacterFacade CharacterPrefab;
            [PrefabAssetSelector] public CharacterModel[] ModelPrefabs;
            [PrefabAssetSelector] [Min(0f)] public float Weight;
        }
    }
}