using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAI : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    public Bullet projectile;
    public float shootDelay;

    public Transform attackPoint;
    Transform player;
    bool attacking;

    public float recoil;

    public float shootRange = 0.5f;

    bool canMove = true;

    public float waitBeforeShot = 1;
    public float shootCooldown = 4;

    public int amountOfShoots = 3;

    public Transform attackOffset;

    public SpriteRenderer visual;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Vector3 dir;
    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            agent.SetDestination(player.position);
        }

        if (Vector2.Distance(transform.position, player.position) < shootRange)
        {
            if(!attacking)
            {
                StartCoroutine(AttackCycle());
            }    
        }

        dir = new Vector3(player.position.x, transform.position.y, player.position.z);

        attackOffset.LookAt(dir);

        if (rb.velocity.x > 0)
        {
            if (Random.Range(0, 100) == 1)
                visual.flipX = !visual.flipX;
        }
    }

        IEnumerator AttackCycle()
        {
            attacking = true;
            canMove = false;
            agent.SetDestination(transform.position);
            yield return new WaitForSeconds(waitBeforeShot);
            for (int i = 0; i < amountOfShoots; i++)
            {
                Bullet bullet = Instantiate(projectile, attackPoint.position, attackPoint.rotation * Quaternion.Euler(1, attackPoint.rotation.y + Random.Range(-recoil, recoil),1));
                bullet.damageLayer = LayerMask.NameToLayer("PlayerDamage");
                yield return new WaitForSeconds(shootDelay);
            }
            canMove = true;
            yield return new WaitForSeconds(shootCooldown);
        attacking = false;
        }
    }

