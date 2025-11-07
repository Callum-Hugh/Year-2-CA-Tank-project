using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int health = 3;
    private bool destroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prefer reading damage from a Projectile component
        Projectile proj = collision.GetComponent<Projectile>();
        if (proj != null)
        {
            health -= proj.damage;
            Debug.Log($"{name} hit by projectile for {proj.damage} dmg. Remaining: {health}");
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
            return;
        }

        // Fallback: tag-based single damage
        if (collision.CompareTag("PlayerProjectile"))
        {
            health--;
            Debug.Log($"{name} hit by tagged projectile. Remaining: {health}");
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
