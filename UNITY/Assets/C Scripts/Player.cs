using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int speed;
    public int directionX ;
    public int directionY ;
    public float timeToAttack;
    public float attackedTimer;

    public float projectileSpeed;

    public int health = 100;

    public int currentHealth;

    public bool alive = true;

    [SerializeField]
    private float rotationSpeed;

    public GameObject projectilePrefab;

    private Animator animator;
    private Vector2 startPosition;
    [Header("Rotation")]
    private GameObject ThunderAreaAttack;

    private Rigidbody2D rb;
    public bool smoothRotation = true;

    private Vector2 lastMovementDirection = Vector2.up;

    public HealthBar healthBar;

    public float attackCoolDown;

    public float attackTimer;

    private AudioSource _audio;

    private bool isPlaying = false;

    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        ThunderAreaAttack = transform.GetChild(0).gameObject;
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        _audio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position = transform.position;
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        position.x = position.x + speed * Time.deltaTime * moveX;
        position.y = position.y + speed * Time.deltaTime * moveY;
        transform.position = position;

        Vector2 movementDirection = new Vector2(moveX, moveY);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


        // When the player moves in a direction the tank rotates in the direction of the movemnt.
        if (movementDirection.sqrMagnitude > 0.0001f)
        {
            movementDirection.Normalize();
            lastMovementDirection = movementDirection;
            transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            //_audio.Play();
            //isPlaying = true;

        }


        if (Input.GetKeyDown(KeyCode.F) && attackedTimer < Time.time)
        {
            Vector2 aimDirection = lastMovementDirection;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Projectile pr = projectile.GetComponent<Projectile>();
            attackedTimer = Time.time + attackCoolDown;

            if (pr != null)
            {
                pr.Launch(aimDirection, projectileSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            TakeDamege(10);
        }
    }


    void TakeDamege(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        alive = false;

        Destroy(gameObject);

        
        
    }

}
