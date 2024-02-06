using RPG.Attributes;
using RPG.Core;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float regenerationPercentage = 100;

        [SerializeField] float health = 0;
        bool bisDead = false;

        private void Awake()
        {
            health = GetComponent<BaseStat>().GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            GetComponent<BaseStat>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStat>().onLevelUp -= RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStat>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            health = Mathf.Max(health, regenHealthPoints);
        }

        public bool IsDead()
        {
            return bisDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);

            health = Mathf.Max(health-damage, 0);
            if (health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStat>().GetStat(Stat.Health);
        }

        public float GetHealthRatio()
        {
            return (health / GetComponent<BaseStat>().GetStat(Stat.Health));
        }


        private void Die()
        {
            if (bisDead) return;

            bisDead = true;
            GetComponent<Animator>().SetTrigger("Dead");
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStat>().GetStat(Stat.ExperienceReward));
        }
    }
}