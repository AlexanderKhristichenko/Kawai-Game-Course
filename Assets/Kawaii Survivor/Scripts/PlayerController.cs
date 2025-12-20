using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] MobileJoystick playerJoystick;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = playerJoystick.GetMoveVector() * moveSpeed;
    }
}
