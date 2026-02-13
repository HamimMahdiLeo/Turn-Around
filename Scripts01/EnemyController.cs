using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float lookStopDistance = 15f; // Only chase when not seen and within this range
    public float moveSpeed = 3f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (!isGrounded)
        {
            animator.speed = 0f; // Stop animation if in air
            return;             // Skip movement if not grounded
        }

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > lookStopDistance)
        {
            // Player too far, stop animation and movement
            animator.speed = 0f;
            return;
        }

        if (IsBeingLookedAt())
        {
            animator.speed = 0f; // Freeze animation & movement
        }
        else
        {
            animator.speed = 1f;

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Flat movement

            Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;
            rb.MovePosition(newPos);

            // Smooth rotation towards player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }

    bool IsBeingLookedAt()
    {
        Vector3 directionToEnemy = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, directionToEnemy);
        return dot > 0.7f;
    }

    // Optional: visualize ground check sphere in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
