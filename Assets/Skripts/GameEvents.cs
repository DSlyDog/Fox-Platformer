using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onLandEvent;
    public event Action onJumpEvent;

    public void onLand()
    {
        if (onLandEvent != null)
        {
            onLandEvent();
        }
    }

    public void onJump()
    {
        print("in onJump");
        if (onJumpEvent != null)
        {
            onJumpEvent();
        }
    }
}
