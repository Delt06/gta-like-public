using AnimationRigging;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Characters
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] [Required] private Animator _animator;
        [SerializeField] [Required] private CharacterBonesInfo _characterBonesInfo;
        [SerializeField] [Required] private RigBuilder _rigBuilder;

        public Animator Animator => _animator;

        public CharacterBonesInfo CharacterBonesInfo => _characterBonesInfo;

        public RigBuilder RigBuilder => _rigBuilder;

        private void Reset()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_characterBonesInfo == null)
            {
                _characterBonesInfo = GetComponent<CharacterBonesInfo>();
                if (_characterBonesInfo == null)
                {
                    _characterBonesInfo = gameObject.AddComponent<CharacterBonesInfo>();
                    FillBones();
                }
            }

            if (_rigBuilder == null)
            {
                _rigBuilder = GetComponent<RigBuilder>();
                if (_rigBuilder == null)
                    _rigBuilder = gameObject.AddComponent<RigBuilder>();
            }
        }

        [Button]
        private void FillBones()
        {
            if (_animator == null)
            {
                Debug.LogError("Animator is not assigned.");
                return;
            }

            if (_characterBonesInfo == null)
            {
                Debug.LogError("Bones info is not assigned.");
                return;
            }

            var leftArm = _characterBonesInfo.LeftArm;
            PopulateArmBones(ref leftArm,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftLowerArm,
                HumanBodyBones.LeftUpperArm
            );
            _characterBonesInfo.LeftArm = leftArm;

            var rightArm = _characterBonesInfo.RightArm;
            PopulateArmBones(ref rightArm,
                HumanBodyBones.RightHand,
                HumanBodyBones.RightLowerArm,
                HumanBodyBones.RightUpperArm
            );
            _characterBonesInfo.RightArm = rightArm;

            var spineBones = _characterBonesInfo.SpineBones;
            spineBones.Head = GetBone(HumanBodyBones.Head);
            spineBones.Neck = GetBone(HumanBodyBones.Neck);
            spineBones.Top = GetBone(HumanBodyBones.UpperChest, HumanBodyBones.Chest);
            _characterBonesInfo.SpineBones = spineBones;
        }

        private void PopulateArmBones(ref ArmBones armBones, HumanBodyBones hand, HumanBodyBones lowerArm,
            HumanBodyBones upperArm)
        {
            armBones.Hand = GetBone(hand);
            armBones.ForeArm = GetBone(lowerArm);
            armBones.Arm = GetBone(upperArm);
        }

        private Transform GetBone(HumanBodyBones bone) => _animator.GetBoneTransform(bone);

        private Transform GetBone(HumanBodyBones bone, HumanBodyBones fallback)
        {
            var boneTransform = _animator.GetBoneTransform(bone);
            return boneTransform ? boneTransform : _animator.GetBoneTransform(fallback);
        }
    }
}