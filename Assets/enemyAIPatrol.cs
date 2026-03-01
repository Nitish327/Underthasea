using UnityEngine;
using UnityEngine.AI;


public class EnemyAIPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
public float patrolRadius = 10f;
public float waitTime = 2f;
public float detectionRange = 10f;
public float patrolSpeed = 3f;
public float chaseSpeed = 5f; // <--- Make sure this line exists!

// ... other variables ...


    
    [Header("Combat Settings")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float damageAmount = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float jumpDetectionDistance = 1f;

    private NavMeshAgent agent;
    private Transform player;
    private float timer;
    private float attackTimer;
    private bool isChasing = false;
    private Rigidbody rb;
    
    // Randomization variables
    private float individualChaseSpeed;
    private float nextJumpTime;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    rb = GetComponent<Rigidbody>();
    
    // 1. Randomize Speed
    individualChaseSpeed = Random.Range(chaseSpeed * 0.8f, chaseSpeed * 1.2f);

    // 2. Randomize Stopping Distance
    // This prevents them from standing in the exact same spot at the player
    agent.stoppingDistance = Random.Range(1.5f, 3.0f);

    // 3. Randomize Start Delay
    // This desyncs their patrol patterns so they don't move in a line
    timer = Random.Range(0, waitTime);

    GameObject target = GameObject.FindGameObjectWithTag("Melee");
    if (target != null) player = target.transform;

    SetNewDestination();
}

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            HandleChase(distanceToPlayer);
        }
        else
        {
            HandlePatrol();
        }
        
        // Check if we need to jump over an obstacle
        TryJump();
    }

    void HandleChase(float distance)
    {
        isChasing = true;
        agent.speed = individualChaseSpeed;

        if (distance <= attackRange)
        {
            // STOP AND ATTACK
            agent.isStopped = true;
            if (Time.time >= attackTimer)
            {
                AttackPlayer();
                attackTimer = Time.time + attackCooldown;
            }
        }
        else
        {
            // KEEP MOVING
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    void AttackPlayer()
{
    if (player != null)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
            Debug.Log(gameObject.name + " attacked the player!");
        }
    }
}

    void HandlePatrol()
    {
        if (isChasing)
        {
            isChasing = false;
            agent.isStopped = false;
            agent.speed = patrolSpeed;
            SetNewDestination();
        }

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

    void TryJump()
{
    // 1. Safety check: If there is no Rigidbody, don't try to jump
    if (rb == null) return;

    RaycastHit hit;
    // Cast ray forward
    if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, jumpDetectionDistance))
    {
        // 2. Check if grounded and cooldown is over
        if (Mathf.Abs(rb.linearVelocity.y) < 0.1f && Time.time > nextJumpTime)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            nextJumpTime = Time.time + 1.5f;
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