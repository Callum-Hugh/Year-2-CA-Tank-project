using UnityEngine;
using UnityEngine.UIElements;

public class Thunder_Area_attack : MonoBehaviour
{
    public Transform attackOrigin;

    public float attackRadius;

    public int damage;

    public LayerMask enemyMask;

    public Enemy_Tank enemy;

   // private Animation animation;

    [Header("Cooldown")]
    public float attackCooldownTime = 2f;

    private float attackCooldownTimer = 0f;

    int active = 0;

    private void Start()
    {
        attackCooldownTimer = 0f;  
        //animation = GetComponent<Animation>();
    }

    private void Update()
    {
        

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (attackCooldownTimer <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            
            

            if (attackOrigin != null)
            {
                Collider2D[] enemysInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);
                foreach (var enemy in enemysInRange)
                {
                    Enemy_Tank enemyTank = enemy.GetComponent<Enemy_Tank>();
                    if (enemyTank != null)
                    {
                        enemyTank.TakeDamage(damage);
                    }
                }

                attackCooldownTimer = attackCooldownTime;
            }
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}