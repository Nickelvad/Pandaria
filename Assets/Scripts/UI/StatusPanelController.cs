using UnityEngine;
using UnityEngine.UI;
using Pandaria;

namespace Pandaria.UI
{
    public class StatusPanelController : MonoBehaviour
    {
        public Slider hpBarSlider;
        public Slider staminaBarSlider;

        void Start()
        {
            EventBus.Instance.CharacterStaminaChanged += OnStaminaChange;
        }

        void OnStaminaChange(object sender, int stamina)
        {
            staminaBarSlider.value = stamina;
        }
    }

}
