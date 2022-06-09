using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public SpriteRenderer spriteRenderer;
    public Sprite idle;
    public Sprite jump;

    public Text text;

    TImer timer;

    Player player;
    private void Start()
    {
        player = new Player(spriteRenderer, controller, idle, jump);
        timer = new TImer(20);
        GameEvents.current.onLandEvent += player.OnLanding;
        GameEvents.current.onJumpEvent += player.OnJumping;
    }

    private void Update()
    {
        player.move();
        if (!timer.isTimerOver())
        {
            text.text = "" + timer.getCurrentTime();
        }
        /*if (water collision check updates to true)
         {
            player.isInWater(true);
         }
        else
        {
            if (player.isInWater())
            {
                player.isInWater(false);
            }
         }*/
    }

    private void FixedUpdate()
    {
        player.checkJump();
    }
}
