using DELTation.LeoEcsExtensions.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Shared
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [SerializeField] [Required] private EntityView _playerPrefab;

        public EntityView PlayerPrefab => _playerPrefab;
    }
}