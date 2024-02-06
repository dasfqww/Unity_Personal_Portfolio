using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        [SerializeField] TextMeshProUGUI healthText;
        Slider healthSlider;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthSlider = GetComponent<Slider>();
        }

        private void Update()
        {
            healthSlider.value = health.GetHealthRatio();
            healthText.text = string.Format("{0:0} / {1:0}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}