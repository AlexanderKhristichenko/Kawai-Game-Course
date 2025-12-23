using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    EnemyMovement movement;

    [Header("Elements")]
    [SerializeField] SpriteRenderer enemyRender;
    [SerializeField] SpriteRenderer spawnIndicator;
    bool hasSpawned;
    Player player;

    [Header("Attack Settings")]
    [SerializeField] int damage;
    [SerializeField] float attackFrequency;
    [SerializeField] float playerDetectionRadius;
    float attackDelay;
    float attackTimer;

    [Header("Effects")]
    [SerializeField] ParticleSystem passAwayParticles;

    [Header("Debug")]
    [SerializeField] bool showGizmos;

    void Start()
    {
        movement = GetComponent<EnemyMovement>();

        player = FindFirstObjectByType<Player>();

        if (player == null) Destroy(gameObject);

        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
    }

    void StartSpawnSequence()
    {
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.3f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f).setLoopPingPong(4).setOnComplete(SpawnComleted);
    }

    void SpawnComleted()
    {
        hasSpawned = true;

        SetRenderersVisibility(true);

        movement.StorePlayer(player);
    }

    void SetRenderersVisibility(bool visibility)
    {
        enemyRender.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    void Update()
    {
        if (!hasSpawned) return;

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

    void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius) Attack();
    }

    void Attack()
    {
        attackTimer = 0;

        player.TakeDamage(damage);
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
