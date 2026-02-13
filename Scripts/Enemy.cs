using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [Header("Enemy Settings (editable in inspector)")]
    public float chaseRange = 15f;
    public float moveSpeed = 3f;
    public float turnSpeed = 120f;
    public float stoppingDistance = 1.2f;
    public float lookThreshold = 0.7f;

    [Header("Attack Settings")]
    public float attackRange = 2f;       // Distance to trigger headbutt
    public float attackCooldown = 1.5f;  // Time between attacks

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
            agent = gameObject.AddComponent<NavMeshAgent>();

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        if (!col)
        {
            col = gameObject.AddComponent<CapsuleCollider>();
            col.height = 2f;
            col.radius = 0.4f;
            col.center = new Vector3(0, 1f, 0);
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.speed = moveSpeed;
        agent.angularSpeed = turnSpeed;
        agent.acceleration = 100f;
        agent.stoppingDistance = stoppingDistance;
        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool beingLookedAt = IsBeingLookedAt();

        // Stop if out of range or player is looking
        if (dist > chaseRange || beingLookedAt)
        {
            agent.isStopped = true;
            agent.ResetPath();
            animator?.SetBool("isWalking", false);
            return;
        }

        // Chase player
        agent.isStopped = false;
        agent.SetDestination(player.position);

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }

        float movementSpeed = agent.velocity.magnitude;
        bool walking = movementSpeed > 0.01f;
        animator?.SetBool("isWalking", walking);

        // Attack / Headbutt + Jumpscare trigger
        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            animator?.SetTrigger("HeadbuttTrigger");
            lastAttackTime = Time.time;

            // --- TRIGGER JUMPSCARE ---
            FindFirstObjectByType<JumpscareManager>().TriggerJumpscare();
        }
    }

    bool IsBeingLookedAt()
    {
        Vector3 dirToEnemy = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, dirToEnemy);
        return dot > lookThreshold;
    }
}
