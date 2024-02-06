using RPG.Attributes;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject equippedWeapon;
        [SerializeField] float attackRange = 2f;
        [SerializeField] float percentageBonus = 0;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile;

        const string weaponName = "Weapon";

        public void SpawnWeapon(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedWeapon)
            {
                Transform handSocket = GetTransform(rightHand, leftHand);

                GameObject weapon = Instantiate(equippedWeapon, handSocket);
                weapon.name = weaponName;
            }

            if (animatorOverride)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handSocket;
            if (isRightHanded) handSocket = rightHand;
            else handSocket = leftHand;
            return handSocket;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return attackRange;
        }

    }
}