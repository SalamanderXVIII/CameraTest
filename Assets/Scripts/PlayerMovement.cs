using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float movementSpeed;
    private Vector2 movementDirection;

    void Start()
    {
        
    }

    void Update()
    {
        MovingPlayer();
    }

    private void MovingPlayer()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerRB.velocity = movementDirection * movementSpeed;
    }
}
