using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerCollision coll;
    private PlayerMove move;
    [HideInInspector]
    public SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<PlayerCollision>();
        move = GetComponentInParent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("canMove", move.canMove);
    }
    public void SetHorizontalMovement(float x, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalVelocity", yVel);
        Flip(x);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
    private void Flip(float state)
    {
        if (state < 0)
            sr.flipX = true;
        else if (state > 0)
            sr.flipX = false;
    }
    private void MoveSound()
    {
        AudioManager.Instance.Play("moveSound");
    }
}
