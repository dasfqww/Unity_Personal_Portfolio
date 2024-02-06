using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStat baseStats;
        TextMeshProUGUI levelText;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStat>();
            levelText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            levelText.text = String.Format("Lv {0:0}", baseStats.GetLevel());
        }
    }
}
