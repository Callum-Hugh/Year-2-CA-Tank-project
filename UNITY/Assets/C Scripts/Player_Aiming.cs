using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Aiming : MonoBehaviour
{

    [SerializeField] private GameObject cannon;

    public GameObject projectilePrefab;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 worldPosition;
    private Vector2 direction;

    // Update is called once per frame
    private void Update()
    {

        CannonRotation();

    }

    //Make cannon rotat
    private void CannonRotation()
    {
       // worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.Readvalue());
        direction = (worldPosition - (Vector2)cannon.transform.position).normalized;
        cannon.transform.up = direction;
    }

}
