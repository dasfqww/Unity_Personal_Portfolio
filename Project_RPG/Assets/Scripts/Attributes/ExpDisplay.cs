using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class ExpDisplay : MonoBehaviour
    {
        Experience experience;
        Text ExpText;

        // Start is called before the first frame update
        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            ExpText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            ExpText.text = string.Format("Exp:{0:0}", experience.GetPoints());
        }
    }

}