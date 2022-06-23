using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveEntity : MonoBehaviour
{
    NavMeshAgent agent;
    private Vector3 spawnPos;
    private Vector3 targetPos;
    public float wanderDistance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(targetPos);
        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
        {
            targetPos = spawnPos + new Vector3(Random.Range(-wanderDistance, wanderDistance), 0, Random.Range(-wanderDistance, wanderDistance));
        }
    }
}
