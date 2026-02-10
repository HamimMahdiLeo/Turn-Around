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
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        if (col == null)
        {
            col = gameObject.AddComponent<CapsuleCollider>();
            col.height = 2f;
            col.radius = 0.4f;
            col.center = new Vector3(0, 1f, 0);
        }

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.speed = moveSpeed;
        agent.angularSpeed = turnSpeed;
        agent.acceleration = 100f;
        agent.stoppingDistance = stoppingDistance;
        agent.updateRotation = false; // manual rotation
        agent.updatePosition = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Check if player is looking at the enemy
        bool beingLookedAt = IsBeingLookedAt();

        if (dist > chaseRange || beingLookedAt)
        {
            // Freeze movement and rotation if being looked at
            agent.isStopped = true;
            agent.ResetPath();

            // Idle animation
            animator?.SetBool("isWalking", false);
            return;
        }

        // Enemy moves toward player
        agent.isStopped = false;
        agent.SetDestination(player.position);

        // ---- ROTATION: only rotate when not being looked at ----
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }

        // ---- WALKING ANIMATION ----
        float movementSpeed = agent.velocity.magnitude;
        bool walking = movementSpeed > 0.01f;
        animator?.SetBool("isWalking", walking);

        // ---- HEADBUTT ATTACK LOGIC ----
        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            animator?.SetTrigger("HeadbuttTrigger");
            lastAttackTime = Time.time;

            // optional: trigger game over
            // GameManager.instance.GameOver();
        }
    }

    bool IsBeingLookedAt()
    {
        Vector3 dirToEnemy = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, dirToEnemy);
        return dot > lookThreshold;
    }
}
