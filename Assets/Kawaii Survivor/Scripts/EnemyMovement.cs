using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float playerDetectionRadius;
    [SerializeField] SpriteRenderer render;
    [SerializeField] SpriteRenderer spawnIndicator;
    bool hasSpawned;

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

        render.enabled = false;
        spawnIndicator.enabled = true;

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.3f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f).setLoopPingPong(4).setOnComplete(SpawnComleted);
    }

    void Update()
    {
        if (!hasSpawned) return;

        FollowPlayer();
        TryAttack();
    }

    void SpawnComleted()
    {
        hasSpawned = true;
        render.enabled = true;
        spawnIndicator.enabled = false;
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
