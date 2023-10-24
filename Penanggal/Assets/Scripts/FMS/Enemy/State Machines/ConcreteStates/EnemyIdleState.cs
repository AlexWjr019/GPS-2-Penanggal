using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyIdleState : EnemyState
{
    private float lookTimer;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Idle state");
    }

    public override void ExitState()
    {
        base.ExitState();

        lookTimer = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Timer & Logic for Searching

        if (lookTimer < enemy.lookDelay)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer > enemy.lookDelay)
            {
                lookTimer = 0;
                enemy.StateMachine.ChangeState(enemy.PatrolState);
            }
        }

        if (lookTimer < (enemy.lookDelay / 2))
        {
            enemy.transform.Rotate(0, enemy.yRotation, 0);
        }
        else if (lookTimer > enemy.lookDelay / 2 - 0.5)
        {
            enemy.transform.Rotate(0, -enemy.yRotation, 0);
        }

        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.Observing();
    }
}
