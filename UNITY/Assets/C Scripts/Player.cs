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
    public float angleOffset = 90f; // add if sprite's art faces up instead of right

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
        movemntDirection.Normalize();

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    GameObject projectile = Instantiate(projectilePrefab, rb.position, Quaternion.identity);
        //     Projectile pr = projectile.GetComponent<Projectile>();
        //       pr.Launch(new Vector2(animator.GetInteger("Direction X"), animator.GetInteger("Direction Y")), 300);
        //     }

        //Rotating the pleyer Got help from chatGPT to roatate
        Vector2 movementDirection = new Vector2(moveX, moveY);

        if (movementDirection.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + 90f; 

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

            if (smoothRotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            else
                transform.rotation = targetRotation;
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
