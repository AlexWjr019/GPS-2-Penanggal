using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyState
{
    float timerCount;

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
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Timer & Logic for Searching

        if (timerCount < enemy.lookTimer)
        {
            timerCount += Time.deltaTime;

            if (timerCount >= enemy.lookTimer)
            {
                timerCount = 0;
                enemy.StateMachine.ChangeState(enemy.PatrolState);
            }
        }

        if (timerCount < (enemy.lookTimer / 2) - 0.5)
        {
            enemy.transform.Rotate(enemy.x, enemy.y, enemy.z);
        }
        else if (timerCount > enemy.lookTimer / 2 - 0.5)
        {
            enemy.transform.Rotate(-enemy.x, -enemy.y, -enemy.z);
        }

        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.Observing();
    }

    //private Vector3 GetRandomPointInCircle()
    //{
    //    return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.randomMovementRange;
    //}
}
