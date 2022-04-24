using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria;


namespace Pandaria.UI
{
    public class ActionBarController : MonoBehaviour
    {
        public Slider slider;

        void Start()
        {
            EventBus.Instance.GatherableResourceProgress += OnGatherableResourceProgress;
            slider.gameObject.SetActive(false);
        }

        void LateUpdate()
        {
            transform.rotation = Camera.main.transform.localRotation;
        }

        public void OnGatherableResourceProgress(object sender, int progress)
        {
            if (progress > 0 && progress < 100)
            {
                if (slider.gameObject.activeSelf != true)
                {
                    slider.gameObject.SetActive(true);
                }
                slider.value = progress;
            }
            else
            {
                if (slider.gameObject.activeSelf == true)
                {
                    slider.gameObject.SetActive(false);
                }
            }
        }
    }

}

