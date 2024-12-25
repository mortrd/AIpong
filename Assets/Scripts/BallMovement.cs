using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initalspeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text PlayerScore;
    [SerializeField] private Text AIScore;

    private int hitcounter;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("starBall", 2f);
    }

    
    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity,initalspeed+hitcounter);
    }
    private void starBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initalspeed + speedIncrease * hitcounter);
    }
    private void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2 (0, 0);
        hitcounter = 0;
        Invoke("starBall", 2f);
    }
    private void PlayerBounce(Transform myobject)
    {
        hitcounter++;

        Vector2 ballpos = transform.position;
        Vector2 playerpos = myobject.position;

        float xdirection, ydirection;
        if(transform.position.x > 0)
        {
            xdirection = -1;
        }
        else
        {
            xdirection = 1;
        }
        ydirection = (ballpos.y - playerpos.y) / myobject.GetComponent<Collider2D>().bounds.size.y;
        if(ydirection == 0)
        {
            ydirection = 0.25f;
        }
        rb.velocity = new Vector2 (xdirection, ydirection)*(initalspeed+(speedIncrease*hitcounter));

     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            Reset();
            PlayerScore.text = (int.Parse(PlayerScore.text)+1).ToString();
        }
        else if (transform.position.x < 0)
        {
            Reset();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }
}
