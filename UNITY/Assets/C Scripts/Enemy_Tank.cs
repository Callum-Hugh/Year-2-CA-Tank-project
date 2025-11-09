using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Tank : MonoBehaviour
{

    //Animator _animator;
    //public float distanceTime;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int health;

    public int currentHealth;

    public HealthBar healthBar;

    public bool isDead = false;

    public float lineOfSight;

    public float shootingRange;

    public float attackCoolDown;

    //[SerializeField]
    //private float moveSpeed;

    //Transform target;

    //[SerializeField]
    //private float rotationSpeed;

    public GameObject EnemyProjectile;

    public GameObject projectileParent;

    private Rigidbody2D rb;
    //private Enemy_Awarness enemyAwarness;

    //private Vector2 targetDirection;

    //private Vector2 moveDirection;

    private Transform player;

    private float attackTimer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        //target = GameObject.Find("Player_Body").transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        /*
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
        */

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }

        else if(distanceFromPlayer <= shootingRange && attackTimer < Time.time)
        {
            Instantiate(EnemyProjectile, projectileParent.transform.position, Quaternion.identity);
            attackTimer = Time.time + attackCoolDown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange); 
    }

/*
    void FixedUpdate()
    {
        
        UpdateTargetDirection();
        RotateTwordsTarget();
        SetVolocity();
    

        if(target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }


    private void UpdateTargetDirection()
    {
        if (enemyAwarness != null && enemyAwarness.AwareOfPlayer)
        {
            // ensure it's a Unity Vector2 and normalize if used for rotation/velocity
            targetDirection = enemyAwarness.DirectionToPlayer;
        }
        else
        {
            // fallback: use patrol direction so enemy still moves when not chasing
            targetDirection = Vector2.zero;
        }
    }

    private void RotateTwordsTarget()
    {
        if (targetDirection != Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVolocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = transform.up * speed;
        }
    }
*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;
        Destroy(gameObject);
    }

}
