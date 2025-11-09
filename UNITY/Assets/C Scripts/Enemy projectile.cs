using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyprojectile : MonoBehaviour
{
    GameObject player;

    public float speed;

    Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        Vector2 direction = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(direction.x, direction.y);
        Destroy(this.gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
