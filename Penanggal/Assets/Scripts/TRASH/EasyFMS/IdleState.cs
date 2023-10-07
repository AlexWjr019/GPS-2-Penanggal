using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool detectPlayer;

    public override State RunCurrentState()
    {
        if(detectPlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
