using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Health.Ui
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] [Required] private Image _fill;

        public void SetFill(float fill)
        {
            _fill.fillAmount = fill;
        }
    }
}