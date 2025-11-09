using System;
using UnityEngine;

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
    private bool AreaAttacking = false;
    public float angleOffset = 90f; 

    public HealthBar healthBar;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        ThunderAreaAttack = transform.GetChild(0).gameObject;
        currentHealth = health;
        healthBar.SetMaxHealth(health);
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

        Vector2 movemntDirection = new Vector2(moveX, moveY);

        Vector2 movementDirection = new Vector2(moveX, moveY);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movemntDirection.Normalize();

        transform.Translate(movemntDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }




        if (Input.GetKeyDown(KeyCode.F))
        {
            // Determine shooting direction: movement direction if moving, otherwise the tank's facing (transform.up)
            Vector2 shootDirection = movementDirection.sqrMagnitude > 0.01f ? movementDirection.normalized : (Vector2)transform.up;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Projectile pr = projectile.GetComponent<Projectile>();
            if (pr != null)
            {
                pr.Launch(shootDirection, projectileSpeed);
            }
        } 

        //Test healthbar
        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    TakeDamege(10);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemyprojectile"))
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

        Debug.Log("Player Dead");
        Destroy(gameObject);
    }

}
