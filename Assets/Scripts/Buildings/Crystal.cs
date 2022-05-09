using System;
using UnityEngine;
using Pandaria.Enemies;

namespace Pandaria.Buildings
{
    public class Crystal : MonoBehaviour
    {
        public GameObject crystalAura;
        public float crystalAuraSize = 5f;
        private ParticleSystem crystalAuraParticleSystem;
        private SphereCollider sphereCollider;
    
        void Awake()
        {
            EventBus.Instance.DayStarted += OnDayStarted;
            EventBus.Instance.NightStarted += OnNightStarted;
            crystalAura.transform.localScale += new Vector3(crystalAuraSize, 0, crystalAuraSize);
            crystalAuraParticleSystem = crystalAura.GetComponent<ParticleSystem>();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = crystalAuraSize * 5;

        }

        public void OnDayStarted(object sender, EventArgs e)
        {
            crystalAura.SetActive(false);
            sphereCollider.enabled = false;
        }

        public void OnNightStarted(object sender, EventArgs e)
        {
            crystalAura.SetActive(true);
            sphereCollider.enabled = true;
        }
    }

}
