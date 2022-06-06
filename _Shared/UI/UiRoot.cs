using Health.Ui;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Shared.UI
{
    public class UiRoot : MonoBehaviour
    {
        [SerializeField] [Required] private Image _crosshairImage;
        [SerializeField] [Required] private HealthBar _playerHealthBar;

        public Image CrosshairImage => _crosshairImage;
        public HealthBar PlayerHealthBar => _playerHealthBar;
    }
}