using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Skills
{
    [CreateAssetMenu(menuName ="Skills/Create Skill")]
    public class Skill : ScriptableObject
    {
        public string SkillName;
        public string Description;
        public Texture Icon;
        public float coolTime;
        public float multipleDamage;

        bool isCoolDown = false;

        public float currentCoolTime { get; private set; }

        WaitForFixedUpdate frame = new WaitForFixedUpdate();

        public event Action<int> OnSkillUsed;

        public void ActivateSkill()
        {
            if (!SkillUsableCheck())
            {
                Debug.Log("Activate" + SkillName);
                
            }              
        }

        public IEnumerator CoolDown(int index)
        {
            if (!SkillUsableCheck())
            {
                isCoolDown = true;
                currentCoolTime = coolTime;
                while (currentCoolTime > 0.0f)
                {
                    currentCoolTime -= Time.deltaTime;
                    OnSkillUsed.Invoke(index);
                    //Debug.Log("Remaining Cooltime:" + currentCoolTime);

                    yield return frame;
                }
                isCoolDown = false;
            }            
        }

        bool SkillUsableCheck()
        {
            return isCoolDown;
        }
    }
}