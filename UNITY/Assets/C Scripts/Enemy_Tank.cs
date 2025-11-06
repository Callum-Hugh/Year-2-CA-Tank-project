using TMPro.EditorUtilities;
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

    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {

            if (isIdle && idleTime < 0)
            {
                directionX = directionX * -1;
                timeInDirection = distanceTime;
                isIdle = false;
                //_animator.SetInteger("Direction X", directionX);
                //_animator.SetInteger("Direction Y", directionY);
                //_animator.SetFloat("Move", 1);
            }

            else if (!isIdle && timeInDirection < 0)
            {
                isIdle = true;
                idleTime = 1;
            }

            if (!isIdle)
            {
                Vector2 pos = transform.position;
                pos.x = pos.x + (speed * Time.deltaTime * directionX);
                transform.position = pos;
                timeInDirection -= Time.deltaTime;
            }

            //if (!isIdle)
            //{
            //    Vector2 pos = transform.position;
            //    pos.y = pos.y + (speed * Time.deltaTime * directionY);
            //    transform.position = pos;
            //    timeInDirection -= Time.deltaTime;
            //}


            else
            {
                idleTime -= Time.deltaTime;
            }

        }




    }
}
