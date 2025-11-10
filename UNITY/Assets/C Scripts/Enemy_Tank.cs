using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Tank : MonoBehaviour
{

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

    private Transform walls;

    private float attackTimer;

    [SerializeField]
    private float rotationSpeed;

    private bool touchingWalls = false;

    private int maxEnemys = 1;

    private AudioSource _audio;

    private bool isPlaying = false;

    public AudioClip collectSound;

    private int enemysDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        player = GameObject.FindWithTag("Player").transform;
        walls = GameObject.FindWithTag("Walls").transform;
        _audio.GetComponent<AudioSource>();
    }

    void Update()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

            //_audio.Play();
            //isPlaying = true;
        }

        else if (distanceFromPlayer <= shootingRange && attackTimer < Time.time)
        {
            Instantiate(EnemyProjectile, projectileParent.transform.position, Quaternion.identity);
            attackTimer = Time.time + attackCoolDown;
        }
        
        if(touchingWalls == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position ,speed * Time.deltaTime);
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

        if(collision.CompareTag("Walls"))
        {
            touchingWalls = true;
        }

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            enemysDead++;
            Dead();

            if(enemysDead > maxEnemys){
                Victory();
            }

        }
    }

    void Victory()
    {
        SceneManager.LoadScene("Menu");
    }

    void Dead()
    {
        isDead = true;
        Destroy(gameObject);
    }

}
