using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    SpriteRenderer sprite;
    Animator anim;
    BoxCollider2D coll;
    float dirx;

    [SerializeField] LayerMask jumpableGround;

    float moveSpeed = 7f;
    float jumpForce = 14f;

    enum MovementState { idle, running, jumping, falling };

    [SerializeField] AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(dirx * moveSpeed, playerRb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        MovementState state;
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (playerRb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (playerRb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
