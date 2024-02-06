using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.Stats
{
    public class ExpDisplay : MonoBehaviour
    {
        Experience exp;
        TextMeshProUGUI expText;
        Slider expSlider;

        private void Awake()
        {
            exp= GameObject.FindWithTag("Player").GetComponent<Experience>();
            expText = GetComponentInChildren<TextMeshProUGUI>();
            expSlider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            expSlider.value = exp.GetExpRatio();
            expText.text = String.Format("{0:0} / {1:0} ({2:F2}%)", exp.GetExp(), exp.GetLevUpExp(), (expSlider.value)*100);
        }
    }
}