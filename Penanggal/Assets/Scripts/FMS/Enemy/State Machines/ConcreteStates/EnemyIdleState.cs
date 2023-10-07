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

        if (timerCount < enemy.lookTimer)
        {
            timerCount += Time.deltaTime;

            if (timerCount >= enemy.lookTimer)
            {
                timerCount = 0;
                enemy.StateMachine.ChangeState(enemy.PatrolState);
            }
        }

        if (timerCount < enemy.lookTimer / 2)
        {
            enemy.transform.Rotate(enemy.x, enemy.y, enemy.z);
        }
        else if (timerCount > enemy.lookTimer / 2)
        {
            enemy.transform.Rotate(-enemy.x, -enemy.y, -enemy.z);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Physics.Raycast(enemy.transform.position, enemy.fwd, out RaycastHit hit, enemy.lookDistance, enemy.layerMask))
        {
            if (hit.collider.name == "Player")
            {
                Debug.Log(hit.collider.gameObject.name + " was hit");
                Debug.DrawRay(enemy.transform.position, enemy.fwd, Color.yellow);
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
    }

    //private Vector3 GetRandomPointInCircle()
    //{
    //    return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.randomMovementRange;
    //}
}
