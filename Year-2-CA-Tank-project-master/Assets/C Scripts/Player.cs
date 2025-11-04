using UnityEngine;

public class Player : MonoBehaviour
{

    public int speed;
    public int directionX = 0;
    public int directionY = 0;

    private int health = 100;

    public GameObject projectilePrefab;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
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

    }

    private void UpdateAnimation(float moveX, float moveY)
    {
        animator.SetFloat("Move Horizontal", moveX);
        animator.SetFloat("Move Vertical", moveY);
        if (moveX > 0)
        {
            animator.SetInteger("Direction X" , 1);
        }
        else if (moveX < 0)
        {
            animator.SetInteger("Direction X", -1);
        }

        if (moveY > 0)
        {
            animator.SetInteger("Direction Y", 1);
        }
        else if (moveY < 0)
        {
            animator.SetInteger("Direction Y", -1);
        }

    }
}
