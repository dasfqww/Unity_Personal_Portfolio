using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Health target;
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = true;
    float damage = 0;
    GameObject instigator;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }


    void Update()
    {
        if (target == null) return;

        if (isHoming)
        {
            if (!target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target, GameObject instigator ,float damage)
    {
        this.target = target;
        this.damage = damage;
        this.instigator = instigator;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead()) return;
        target.TakeDamage(instigator ,damage);
        Destroy(gameObject);
    }
}
