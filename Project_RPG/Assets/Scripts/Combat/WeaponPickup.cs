using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using System;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;

        WaitForSeconds showWait;
        Collider collider;

        private void Awake()
        {
            collider = GetComponent<Collider>();
            showWait = new WaitForSeconds(respawnTime);
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return showWait;
            ShowPickup(true);
        }

        void ShowPickup(bool shouldShow)
        {
            collider.enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}