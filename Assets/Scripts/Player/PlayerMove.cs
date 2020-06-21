using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerCollision coll;
    private PlayerAnimation anim;

    [Space]

    [Header("Particles")]
    public ParticleSystem jumpParticle;
    public ParticleSystem walkParticle;

    [Space]

    [Header("Stats")]
    public float speed = 10f;
    public float jumpForce = 50;
    public bool canMove;

    [Space]

    private bool groundTouch;
    Vector2 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PlayerCollision>();
        anim = GetComponentInChildren<PlayerAnimation>();
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal_L");
        float y = Input.GetAxis("Vertical");
        dir = new Vector2(x, y);

        if (Input.GetButtonDown("Jump") && GameManager.Instance.GameState == GameState.Gaming)
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up);
        }
    }
    private void FixedUpdate()
    {
        Walk(dir);
        anim.SetHorizontalMovement(dir.x, rb.velocity.y);

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }
    
    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        if (dir.x != 0 && coll.onGround)
            walkParticle.Play();
        else
            walkParticle.Stop();
    }
    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        jumpParticle.Play();
    }
    private void GroundTouch()
    {
        jumpParticle.Play();
    }
}
