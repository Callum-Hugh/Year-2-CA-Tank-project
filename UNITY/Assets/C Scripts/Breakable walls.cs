using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakablewalls : MonoBehaviour
{

    public int health = 20;

    private bool destroyed = false;

    // Update is called once per frame
    void Update()
    {

        if (destroyed == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health--;
            if (health < 0)
            {
                destroyed = true;
            }
        }
    }
}
