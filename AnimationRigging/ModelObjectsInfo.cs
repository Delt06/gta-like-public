using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;

namespace AnimationRigging
{
    public class ModelObjectsInfo : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _lookAt;
        [SerializeField] [Required] private Transform _lookAtAimTarget;
        [SerializeField] [Required] private Transform _weapons;
        [SerializeField] [Required] private Transform _commonHandleRight;
        [SerializeField] [Required] private Transform _commonHandleLeft;
        [SerializeField] [Required] private Weapon _pistol;

        public Transform LookAt => _lookAt;
        public Transform LookAtAimTarget => _lookAtAimTarget;
        public Transform Weapons => _weapons;
        public Transform CommonHandleRight => _commonHandleRight;
        public Transform CommonHandleLeft => _commonHandleLeft;
        public Weapon Pistol => _pistol;
    }
}