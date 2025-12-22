using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;

    [Header("Elements")]
    Player player;

    void Update()
    {
        if (player != null) FollowPlayer();
    }

    public void StorePlayer(Player player) => this.player = player;

    void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        transform.position = targetPosition;
    }


}
