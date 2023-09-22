using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForTest : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 movementDirection;

    void Update()
    {
        MovingPlayer();
    }

    private void MovingPlayer()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GetComponent<Rigidbody2D>().velocity = movementDirection * movementSpeed;
    }
}
