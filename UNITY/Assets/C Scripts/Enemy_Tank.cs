using UnityEngine;

public class Enemy_Tank : MonoBehaviour
{

    Animator _animator;

    public int directionX = 0;
    public int directionY = 0;
    public float distanceTime;
    public float speed;
    public int health;

    private float timeInDirection;
    private bool isDead = false;
    private int moveTimer = 3;
    private bool isIdle = false;
    private float idleTime = 1;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rb;

    private Enemy_Awarness ea;
    private Enemy_Awarness enemyAwarness;

    private Vector2 targetDirection;

    // Start is called before the first frame update
    //void Start()
    //{
    //_animator = GetComponent<Animator>();
    //    timeInDirection = distanceTime;
    //}

    // Update is called once per frame

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAwarness = GetComponent<Enemy_Awarness>();
    }

    void Update()
    {
        //  if (!isDead)
        //  {

        //      if (isIdle && idleTime < 0)
        //      {
        //        directionX = directionX * -1;
        //       timeInDirection = distanceTime;
        //       isIdle = false;
        //_animator.SetInteger("Direction X", directionX);
        //_animator.SetInteger("Direction Y", directionY);
        //_animator.SetFloat("Move", 1);
        // }

        //else if (!isIdle && timeInDirection < 0)
        //{
        //  isIdle = true;
        // idleTime = 1;
        //}

        //if (!isIdle)
        //{
        //    Vector2 pos = transform.position;
        //    pos.x = pos.x + (speed * Time.deltaTime * directionX);
        //    transform.position = pos;
        //    timeInDirection -= Time.deltaTime;
        //}

        //if (!isIdle)
        //{
        //    Vector2 pos = transform.position;
        //    pos.y = pos.y + (speed * Time.deltaTime * directionY);
        //    transform.position = pos;
        //    timeInDirection -= Time.deltaTime;
        //}


        //        else
        //        {
        //            idleTime -= Time.deltaTime;
        //        }

        //    }
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTwordsTarget();
        SetVolocity();
    }

    private void UpdateTargetDirection()
    {
        if (Enemy_Awarness.AwareOfPlayer)
        {
            targetDirection = Enemy_Awarness.DirectionToPlayer;
        }

        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void RotateTwordsTarget()
    {
        if (targetDirection != Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transfrom.forword, targetDirection);
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
           rb.velocity = transform.up * speed
        }
    }    

}
