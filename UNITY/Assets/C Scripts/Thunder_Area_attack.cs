using UnityEngine;
using UnityEngine.UIElements;

public class Thunder_Area_attack : MonoBehaviour
{
    public Transform attackOrigin;

    public float attackRadius;

    public int damage;

    public Enemy_Tank enemy;

    public float attackCooldownTime;

    public float attackCooldownTimer;

    private void Update()
    {
        if (attackCooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                enemy.GetComponent<Enemy_Tank>().TakeDamage(damage);
            }

            attackCooldownTimer = attackCooldownTime;
        }

        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}
