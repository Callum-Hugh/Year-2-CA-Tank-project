using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int health = 3;
    private bool destroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj = collision.GetComponent<Projectile>();
        if (proj != null)
        {
            health -= 10;
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
            return;
        }

        if (collision.CompareTag("Projectile"))
        {
            health--;
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
