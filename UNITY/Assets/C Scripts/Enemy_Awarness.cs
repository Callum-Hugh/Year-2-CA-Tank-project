using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Awarness : MonoBehaviour
{

    public bool AwareOfPlayer { get; private set; }

    [SerializeField]
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float awaerenessDistance;

    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyPositionToPlayer = player.position - transform.position;
        DirectionToPlayer = enemyPositionToPlayer.normalized;

        if (enemyPositionToPlayer.magnitude <= awaerenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;  
        }
    }
}
