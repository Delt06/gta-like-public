using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Shared
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] [Required] private Camera _camera;
        [SerializeField] [Required] private CinemachineBrain _cinemachineBrain;
        [SerializeField] [Required] private CinemachineVirtualCameraBase _mainVirtualCamera;
        [SerializeField] [Required] private CinemachineVirtualCameraBase _carVirtualCamera;
        [SerializeField] [Required] private CinemachineVirtualCameraBase _aimVirtualCamera;
        [SerializeField] [Required] private Transform _cameraLookAt;
        [SerializeField] [Required] private Transform _playerSpawnPoint;
        [SerializeField] [Required] private Transform _npcSpawnPointsRoot;

        public Camera Camera => _camera;

        public CinemachineBrain CinemachineBrain => _cinemachineBrain;

        public CinemachineVirtualCameraBase MainVirtualCamera => _mainVirtualCamera;

        public CinemachineVirtualCameraBase CarVirtualCamera => _carVirtualCamera;

        public CinemachineVirtualCameraBase AimVirtualCamera => _aimVirtualCamera;

        public Transform CameraLookAt => _cameraLookAt;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        public Transform NpcSpawnPointsRoot => _npcSpawnPointsRoot;
    }
}