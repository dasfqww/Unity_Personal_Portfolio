using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Attributes
{
    public class SkillCoolTimeDisplay : MonoBehaviour
    {
        [SerializeField] List<Skill> skills = new List<Skill>();
        [SerializeField] List<RawImage> skillImages = new List<RawImage>();       

        // Start is called before the first frame update
        void Awake()
        {
            if (skills.Count != skillImages.Count) return;

            for (int i = 0; i < skills.Count; i++)
            {
                skillImages[i].texture = skills[i].Icon;
                SetShowCoolDownUI(i, false);
                skills[i].OnSkillUsed += (i) => CoolTimeDisplay(i);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].OnSkillUsed -= (i) => CoolTimeDisplay(i);
            }
        }

        void CoolTimeDisplay(int index)
        {
            SetShowCoolDownUI(index, true);
            skillImages[index].GetComponentInChildren<Image>().fillAmount
                = skills[index].currentCoolTime / skills[index].coolTime;
            skillImages[index].GetComponentInChildren<TextMeshProUGUI>().text =
                string.Format("{0:0}s", skills[index].currentCoolTime);
            
            if (skills[index].currentCoolTime<0.0f)
            {
                skillImages[index].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }

        private void SetShowCoolDownUI(int index, bool isShow)
        {
            skillImages[index].GetComponentInChildren<Image>().enabled = isShow;
            skillImages[index].GetComponentInChildren<TextMeshProUGUI>().enabled = isShow;
        }
    }
}