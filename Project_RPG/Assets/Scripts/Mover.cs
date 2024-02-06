using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Core;
using RPG.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent agent;
        Animator animator;
        ActionScheduler actionScheduler;
        Health health;

        [SerializeField] AudioClip[] runFootStepSfxs;
        [SerializeField] AudioClip[] walkFootStepSfxs;

        // Start is called before the first frame update
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.IsDead();

            UpdateAnim();
        }

        void UpdateAnim()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("Speed", speed);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            actionScheduler.StartAction(this);
            MoveToDestination(destination, speedFraction);
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        public void MoveToDestination(Vector3 dest, float speedFraction)
        {
            agent.SetDestination(dest);
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
        }

        public void PlayFootStepSfx(string moveState)
        {
            switch (moveState)
            {
                case "Walk":
                    PlayRandomSfx(walkFootStepSfxs);
                    break;
                case "Run":
                    PlayRandomSfx(runFootStepSfxs);
                    break;
            }
            
        }

        private void PlayRandomSfx(AudioClip[] clips)
        {
            int selection = Random.Range(0, clips.Length);
            AudioClip audio = clips[selection];

            Managers.Sound.Play(audio, SoundManager.Sound.Sfx);
        }
    }

}