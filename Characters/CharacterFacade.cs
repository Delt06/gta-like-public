using DELTation.LeoEcsExtensions.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters
{
    public class CharacterFacade : MonoBehaviour
    {
        [SerializeField] [Required] private EntityView _entityView;
        [SerializeField] [Required] private Transform _modelRoot;

        public EntityView EntityView => _entityView;

        public Transform ModelRoot => _modelRoot;
    }
}