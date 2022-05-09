using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Lighting
{
    [ExecuteAlways]
    public class LightingManager : Singleton<LightingManager>
    {
        [SerializeField] private Light directionalLight;
        [SerializeField] private LightingPreset preset;
        [SerializeField] public float fullDayTimeSeconds = 60f;
        [SerializeField, Range(0f, 1f)] [ReadOnly] public float timeOfDay;
        [SerializeField, Range(0f, 1f)] [ReadOnly] public float nightTime = 0.7f;
        [SerializeField, Range(0f, 0.3f)] [ReadOnly] public float dayNightTransitionTime = 0.05f;
        private float timeNow = 0f;
        [SerializeField, Range(0f, 360f)] public float rotation = 100;
        private bool nightTimeStarted = false;
        private bool dayTimeStarted = false;

        void Awake()
        {
            foreach (Gradient color in new List<Gradient>{preset.AmbientColor, preset.DirectionalColor})
            {
                color.colorKeys[1].time = dayNightTransitionTime;
                color.colorKeys[2].time = nightTime;
                color.colorKeys[3].time = nightTime + dayNightTransitionTime;
            }
        }

        void Update()
        {
            if (preset == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                timeNow += Time.deltaTime;
                if (timeNow > fullDayTimeSeconds)
                {
                    timeNow = 0;
                }
                timeOfDay = timeNow / fullDayTimeSeconds;

                if (timeOfDay < nightTime && !dayTimeStarted)
                {
                    dayTimeStarted = true;
                    nightTimeStarted = false;
                    EventBus.Instance.CallDayStarted(this);
                }

                if (timeOfDay >= nightTime && !nightTimeStarted)
                {
                    dayTimeStarted = false;
                    nightTimeStarted = true;
                    EventBus.Instance.CallNightStarted(this);
                }
            }

            UpdateLighting(timeOfDay);
        }

        private void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
            if (directionalLight != null)
            {
                directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(
                    new Vector3((timePercent * 180f), rotation, 0)
                );
            }
        }
        private void OnValidate()
        {
            if (directionalLight != null)
            {
                return;
            }

            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                        return;
                    }
                }
            }
        }
    }

}
