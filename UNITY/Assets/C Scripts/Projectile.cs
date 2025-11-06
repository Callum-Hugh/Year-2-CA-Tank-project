using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    //Look back why need for vector2
    public void Launch(UnityEngine.Vector2 direction, float force)
    {
        rbody.AddForce(direction * force);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}