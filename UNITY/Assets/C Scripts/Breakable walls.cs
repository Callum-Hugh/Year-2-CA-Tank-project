using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int health = 3;
    private bool destroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj = collision.GetComponent<Projectile>();
        Enemyprojectile enemyProj = collision.GetComponent<Enemyprojectile>();

        if (proj != null)
        {
            health -= 10;
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
        }

        if(enemyProj != null)
        {
            health -= 5;
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
        }

    }
}
