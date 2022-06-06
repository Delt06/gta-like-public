using Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace AnimationRigging
{
    public class CharacterRigsRoot : MonoBehaviour
    {
        [SerializeField] [Required] private Rig[] _rigs;
        [SerializeField] [Required] private TwoBoneIKConstraint[] _leftArmConstraints;
        [SerializeField] [Required] private TwoBoneIKConstraint[] _rightArmConstraints;
        [SerializeField] [Required] private MultiAimConstraint[] _topSpineAimConstraints;
        [SerializeField] [Required] private MultiAimConstraint[] _neckAimConstraints;
        [SerializeField] [Required] private MultiAimConstraint[] _headAimConstraints;
        [SerializeField] [Required] private MultiPositionConstraint[] _lookAtObjectConstraints;
        [SerializeField] [Required] private MultiAimConstraint[] _lookAtObjectSourceConstraints;
        [SerializeField] [Required] private MultiAimConstraint[] _weaponsAimConstraints;
        [SerializeField] [Required] private MultiParentConstraint[] _commonHandleRightSourceConstraints;
        [SerializeField] [Required] private MultiParentConstraint[] _commonHandleLeftSourceConstraints;
        [SerializeField] [Required] private MultiPositionConstraint[] _lookAtAimTargetSourceConstraints;
        [SerializeField] [Required] private Rig _aimRig;
        [SerializeField] [Required] private Rig _handsIkRig;

        public Rig AimRig => _aimRig;

        public Rig HandsIkRig => _handsIkRig;

        [Button]
        public void Init(CharacterModel characterModel, ModelObjectsInfo modelObjectsInfo)
        {
            var rigBuilder = characterModel.RigBuilder;

            foreach (var rig in _rigs)
            {
                rigBuilder.layers.Add(new RigLayer(rig));
            }

            var characterBonesInfo = characterModel.CharacterBonesInfo;

            InitTwoBoneIkConstraints(_leftArmConstraints, characterBonesInfo.LeftArm);
            InitTwoBoneIkConstraints(_rightArmConstraints, characterBonesInfo.RightArm);

            var spineBones = characterBonesInfo.SpineBones;
            InitAimConstraints(_topSpineAimConstraints, spineBones.Top, spineBones.TopAxes);
            InitAimConstraints(_neckAimConstraints, spineBones.Neck, spineBones.NeckAxes);
            InitAimConstraints(_headAimConstraints, spineBones.Head, spineBones.HeadAxes);

            foreach (var lookAtObjectConstraint in _lookAtObjectConstraints)
            {
                lookAtObjectConstraint.data.constrainedObject = modelObjectsInfo.LookAt;
            }

            foreach (var weaponsAimConstraint in _weaponsAimConstraints)
            {
                weaponsAimConstraint.data.constrainedObject = modelObjectsInfo.Weapons;
            }

            foreach (var lookAtObjectSourceConstraint in _lookAtObjectSourceConstraints)
            {
                SetSingleSourceObject(ref lookAtObjectSourceConstraint.data, modelObjectsInfo.LookAt);
            }

            foreach (var commonHandleRightSource in _commonHandleRightSourceConstraints)
            {
                SetSingleSourceObject(ref commonHandleRightSource.data, modelObjectsInfo.CommonHandleRight);
            }

            foreach (var commonHandleLeftSource in _commonHandleLeftSourceConstraints)
            {
                SetSingleSourceObject(ref commonHandleLeftSource.data, modelObjectsInfo.CommonHandleLeft);
            }

            foreach (var lookAtAimTargetSourceConstraint in _lookAtAimTargetSourceConstraints)
            {
                SetSingleSourceObject(ref lookAtAimTargetSourceConstraint.data, modelObjectsInfo.LookAtAimTarget);
            }

            rigBuilder.Build();
        }

        private static void SetSingleSourceObject(ref MultiAimConstraintData data, Transform sourceObject)
        {
            var sourceObjects = data.sourceObjects;
            sourceObjects[0] = new WeightedTransform
            {
                transform = sourceObject,
                weight = 1f,
            };
            data.sourceObjects = sourceObjects;
        }

        private static void SetSingleSourceObject(ref MultiParentConstraintData data, Transform sourceObject)
        {
            var sourceObjects = data.sourceObjects;
            sourceObjects[0] = new WeightedTransform
            {
                transform = sourceObject,
                weight = 1f,
            };
            data.sourceObjects = sourceObjects;
        }

        private static void SetSingleSourceObject(ref MultiPositionConstraintData data, Transform sourceObject)
        {
            var sourceObjects = data.sourceObjects;
            sourceObjects[0] = new WeightedTransform
            {
                transform = sourceObject,
                weight = 1f,
            };
            data.sourceObjects = sourceObjects;
        }

        private static void InitTwoBoneIkConstraints(TwoBoneIKConstraint[] twoBoneIKConstraints, ArmBones arm)
        {
            foreach (var twoBoneIKConstraint in twoBoneIKConstraints)
            {
                ref var data = ref twoBoneIKConstraint.data;
                data.root = arm.Arm;
                data.mid = arm.ForeArm;
                data.tip = arm.Hand;
            }
        }

        private static void InitAimConstraints(MultiAimConstraint[] multiAimConstraints, Transform bone,
            SpineBoneAxes axes)
        {
            foreach (var aimConstraint in multiAimConstraints)
            {
                ref var data = ref aimConstraint.data;
                data.constrainedObject = bone;
                data.aimAxis = axes.AimAxis;
                data.upAxis = axes.UpAxis;
            }
        }
    }
}