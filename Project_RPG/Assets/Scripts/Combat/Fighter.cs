using RPG.Movement;
using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, IModifierProvider
    {
        
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandSocket;
        [SerializeField] Transform leftHandSocket;
        [SerializeField] Weapon defaultWeapon;

        float timeSinceLastAttack = Mathf.Infinity;

        Health target;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;
        Weapon currentWeapon;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();

            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!target) return;

            if (target.IsDead()) return;

            if (!GetIsinRange())
            {
                mover.MoveToDestination(target.transform.position, 1f);
            }

            else
            {
                mover.Cancel();
                Attack();
            }
        }

        void Shoot()
        {
            Hit();
        }

        private void Attack()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                transform.LookAt(target.transform);
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("StopAttack");
            animator.SetTrigger("Attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health target = combatTarget.GetComponent<Health>();
            return target && !target.IsDead();
        }

        void Hit()
        {
            if (!target) return;

            float damage = GetComponent<BaseStat>().GetStat(Stat.Damage);

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandSocket, leftHandSocket, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, currentWeapon.GetDamage());
            }
        }

        private bool GetIsinRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            
            weapon.SpawnWeapon(rightHandSocket, leftHandSocket, animator);
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }
    }
}

