using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float exp = 0;

        float levUpExp=0;

        public event Action onExperienceGained;

        private void Awake()
        {
            levUpExp = GetComponent<BaseStat>().GetStat(Stat.ExperienceToLevelUp);
        }

        public float GetExp()
        {
            return exp;
        }

        public float GetLevUpExp()
        {
            return levUpExp;
        }

        public float GetExpRatio()
        {
            return (exp / GetComponent<BaseStat>().GetStat(Stat.ExperienceToLevelUp));
        }

       /* public float GetExpPercent()
        {
            return (exp / GetComponent<BaseStat>().GetStat(Stat.ExperienceToLevelUp));
        }*/

        public void GainExperience(float experience)
        {
            exp += experience;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return exp;
        }
    }
}
