using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class CopyPosition : MonoBehaviour
    {
        [SerializeField] bool x, y, z;
        [SerializeField] Transform target;

        private void Start()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!target) return;
            
                transform.position = new Vector3(
                    (x? target.position.x:transform.position.x),
                    (y? target.position.y:transform.position.y),
                    (z? target.position.z:transform.position.z));
            
        }
    }
}