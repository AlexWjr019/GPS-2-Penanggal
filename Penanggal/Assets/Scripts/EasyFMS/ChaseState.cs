using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public bool inRange;

    public override State RunCurrentState()
    {
        if(inRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
