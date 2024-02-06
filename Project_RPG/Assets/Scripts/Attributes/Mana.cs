using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float mana=0;

        // Start is called before the first frame update
        void Start()
        {
            mana= GetComponent<BaseStat>().GetStat(Stat.Mana);
        }

        // Update is called once per frame
        void Update()
        {
            if (mana < GetMaxMana())
            {
                mana += GetRegenRate() * Time.deltaTime;
                if (mana > GetMaxMana())
                {
                    mana = GetMaxMana();
                }
            }
        }

        public float GetManaRatio()
        {
            return (mana / GetComponent<BaseStat>().GetStat(Stat.Mana));
        }

        public float GetMana()
        {
            return mana;
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStat>().GetStat(Stat.Mana);
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStat>().GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana)
            {
                return false;
            }
            mana -= manaToUse;
            return true;
        }
    }

}