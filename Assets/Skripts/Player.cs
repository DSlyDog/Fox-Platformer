using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    private CharacterController controller;
    private SpriteRenderer spriteRenderer;
    public Sprite idle;
    public Sprite jumping;

    private float runSpeed = 30;
    private float horizontalMove = 0f;

    private bool jump = false;
    private bool inWater = false;

    public Player(SpriteRenderer spriteRenderer, CharacterController controller, Sprite idle, Sprite jumping)
    {
        this.controller = controller;
        this.spriteRenderer = spriteRenderer;
        this.idle = idle;
        this.jumping = jumping;
    }

    public void move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (inWater)
        {
            //if timer running
            //do nothing
            //else
            //start timer
        }
        else
        {
            //if timer is running
            //cancel it
        }
    }

    public void checkJump()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void OnLanding()
    {
        spriteRenderer.sprite = idle;
    }

    public void OnJumping()
    {
        spriteRenderer.sprite = jumping;
    }

    public bool isInWater()
    {
        return inWater;
    }

    public void isInWater(bool value)
    {
        this.inWater = value;
    }
}
