using UnityEngine;

public class Weapon : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Enemy closestEnemy = null;
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        float minDistance = 5000;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i];
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        if (closestEnemy == null)
        {
            transform.up = Vector3.up;
            return;
        }

        transform.up = (closestEnemy.transform.position - transform.position).normalized;
    }
}
