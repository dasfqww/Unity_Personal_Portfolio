using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Manager;

namespace RPG.UI
{
    public class DisplayMinimap : MonoBehaviour
    {
        [SerializeField] Camera minimapCamera;

        TextMeshProUGUI mapNameText;

        private void Start()
        {
            mapNameText = GetComponentInChildren<TextMeshProUGUI>();
            mapNameText.text = Managers.Scene.GetSceneName();
        }
    }
}