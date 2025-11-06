using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rbody;

    [Tooltip("Seconds before the projectile is automatically destroyed")]
    public float lifeTime = 3f;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Launch the projectile in a direction. `speed` is treated as units/sec velocity.
    public void Launch(Vector2 direction, float speed)
    {
        if (rbody == null)
            rbody = GetComponent<Rigidbody2D>();

        rbody.velocity = direction.normalized * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}