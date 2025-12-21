using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float playerDetectionRadius;

    [Header("Effects")]
    [SerializeField] ParticleSystem passAwayParticles;

    [Header("Debug")]
    [SerializeField] bool showGizmos;

    Player player;

    void Start()
    {
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius) PassAway();
    }

    void PassAway()
    {
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
