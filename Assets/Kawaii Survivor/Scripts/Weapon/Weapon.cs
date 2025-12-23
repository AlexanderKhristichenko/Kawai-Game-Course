using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform enemy;

    void Start()
    {

    }

    void Update()
    {
        transform.up = (enemy.transform.position - transform.position).normalized;
    }
}
