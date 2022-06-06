using UnityEngine;

namespace AnimationRigging
{
    public class CharacterBonesInfo : MonoBehaviour
    {
        [SerializeField] private ArmBones _leftArm;
        [SerializeField] private ArmBones _rightArm;
        [SerializeField] private SpineBones _spine = SpineBones.Default;

        public ArmBones LeftArm
        {
            get => _leftArm;
            set => _leftArm = value;
        }

        public ArmBones RightArm
        {
            get => _rightArm;
            set => _rightArm = value;
        }

        public SpineBones SpineBones
        {
            get => _spine;
            set => _spine = value;
        }
    }
}