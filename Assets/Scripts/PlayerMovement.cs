using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;

    private Rigidbody2D Rb;
    private Vector2 PlayerMove;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isAI)
        {
            aicontrol();
        }
        else
        {
            playerControl();
        }
    }
    private void playerControl()
    {
        PlayerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }
    private void aicontrol ()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            PlayerMove = new Vector2(0, 1);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            PlayerMove= new Vector2(0, -1);
        }
        else
        {
             PlayerMove = new Vector2 (0, 0);
        }
    }
    private void FixedUpdate()
    {
        Rb.velocity = PlayerMove * moveSpeed;
    }
}
