using UnityEngine;
using UnityEngine.AI;

public class enemyAIPatrol : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float waitTime = 2f;

    public float detectionRange = 8f;
    public float chaseSpeed = 5f;
    public float patrolSpeed = 3f;

    private NavMeshAgent agent;
    private Transform player;
    private float timer;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 👇 Changed tag here
        GameObject target = GameObject.FindGameObjectWithTag("Melee");

        if (target != null)
        {
            player = target.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Melee' found!");
        }

        agent.speed = patrolSpeed;
        timer = waitTime;
        SetNewDestination();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                agent.speed = patrolSpeed;
                SetNewDestination();
            }

            Patrol();
        }
    }

    void Patrol()
    {
        timer += Time.deltaTime;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (timer >= waitTime)
            {
                SetNewDestination();
                timer = 0;
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}