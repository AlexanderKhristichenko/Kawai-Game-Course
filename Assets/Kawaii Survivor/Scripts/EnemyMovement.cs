using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float playerDetectionRadius;
    Player player;
    bool hasSpawned;

    [Header("Elements")]
    [SerializeField] SpriteRenderer enemyRender;
    [SerializeField] SpriteRenderer spawnIndicator;

    [Header("Effects")]
    [SerializeField] ParticleSystem passAwayParticles;

    [Header("Attack Settings")]
    [SerializeField] int damage;
    [SerializeField] float attackFrequency;
    float attackDelay;
    float attackTimer;

    [Header("Debug")]
    [SerializeField] bool showGizmos;

    void Start()
    {
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Destroy(gameObject);
        }

        enemyRender.enabled = false;
        spawnIndicator.enabled = true;

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.3f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f).setLoopPingPong(4).setOnComplete(SpawnComleted);

        attackDelay = 1f / attackFrequency;
    }

    void Update()
    {
        if (!hasSpawned) return;

        FollowPlayer();

        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
    }

    void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    void SpawnComleted()
    {
        hasSpawned = true;
        enemyRender.enabled = true;
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

        if (distanceToPlayer <= playerDetectionRadius) Attack();
    }

    void Attack()
    {
        Debug.Log("Attack");
        attackTimer = 0;
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
