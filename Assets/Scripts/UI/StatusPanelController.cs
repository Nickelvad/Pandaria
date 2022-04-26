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
            EventBus.Instance.CharacterHpChanged += OnHpChange;
        }

        void OnStaminaChange(object sender, int stamina)
        {
            staminaBarSlider.value = stamina;
        }

        void OnHpChange(object sender, int hp)
        {
            hpBarSlider.value = hp;
        }
    }

}
