using UnityEngine;
using UnityEngine.UI;
using Pandaria.Characters;
namespace Pandaria.UI.Character
{
    public class CharacterWindowController : MonoBehaviour
    {
        public Button closeButton;
        public Pandaria.Characters.CharacterController characterController;

        void Awake()
        {
            closeButton.onClick.AddListener(CloseClick);
        }

        public void CloseClick()
        {
            gameObject.SetActive(false);
        }
    }

}
