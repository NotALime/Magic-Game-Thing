using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public float damage;
    float speed;
    public float attackDistance;

    public float hitRange;
    public Transform attackPoint;

    public LayerMask damageLayers;

    private bool attacking;

    float baseSpeed;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        baseSpeed = speed;
    }

    private void Update()
    {
        agent.speed = speed;
        agent.SetDestination(player.position);

        transform.rotation = player.rotation;

        if (Vector2.Distance(transform.position, player.position) < attackDistance)
        {
            if (!attacking)
                StartCoroutine(AttackCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        attacking = true;
        speed = 0;
        yield return new WaitForSeconds(1f);
        Attack();
        yield return new WaitForSeconds(0.5f);
        speed = baseSpeed;
        attacking = false;
    }

    void Attack()
    {
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, hitRange, damageLayers);
        foreach (Collider hitObject in hitObjects)
        {
            if (hitObject.gameObject != gameObject)
            {
                hitObject.GetComponent<damagableObject>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, hitRange);
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }


}
