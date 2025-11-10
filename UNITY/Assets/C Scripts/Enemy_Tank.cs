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

    public float shootingRange2;

    public float attackCoolDown;

    public GameObject EnemyProjectile;

    public GameObject projectileParent;

    private Rigidbody2D rb;

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
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        

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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange2);
    }

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
