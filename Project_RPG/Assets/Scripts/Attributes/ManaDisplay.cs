using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;
        Slider manaSlider;
        TextMeshProUGUI manaText;

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>();
            manaSlider = GetComponent<Slider>();
            manaText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            manaSlider.value = mana.GetManaRatio();
            manaText.text = String.Format("{0:0} / {1:0}", mana.GetMana(), mana.GetMaxMana());
        }
    }
}