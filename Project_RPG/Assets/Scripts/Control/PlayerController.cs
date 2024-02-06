using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using RPG.Attributes;
using RPG.Skills;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Health health;
        Fighter fighter;

        [SerializeField] List<Skill> skills = new List<Skill>();

        [System.Serializable]  
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings;

        void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            
        }

        // Update is called once per frame
        void Update()
        {
           
            if (health.IsDead()) 
            {
                SetCursor(CursorType.None);
                return;                
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseSkill(0);
            }

            if (InteractWithComponent())return;
            if (InputMovement())return;
            if (CursorWithUI()) return;
            SetCursor(CursorType.None);
        }

        void UseSkill(int index)
        {
            StartCoroutine(skills[index].CoolDown(index));
            skills[index].ActivateSkill();
        }

        private bool CursorWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }


        bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private bool InputMovement()
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(GetMouseRay(), out hit);
            if (isHit)
            {
                if (Input.GetMouseButton(1))
                {
                    mover.StartMoveAction(hit.point, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}