using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
                
        GotoNextPoint();

        //enemy.PlayHumming();

        Debug.Log("Patrol state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            if (enemy.StateMachine.CurrentEnemyState == enemy.IdleState)
            {
                return;
            }
            else
            {
                enemy.StateMachine.ChangeState(enemy.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.Observing();
    }

    private void GotoNextPoint()
    {
        if (enemy.points.Count == 0)
        {
            return;
        }

        enemy.agent.destination = enemy.points[enemy.destPoint].position;

        enemy.destPoint = (enemy.destPoint + 1) % enemy.points.Count;
    }

    //private void FOVCheck()
    //{
    //    Collider[] rangeChecks = Physics.OverlapSphere(enemy.transform.position, enemy.radius, enemy.targetMask);

    //    if (rangeChecks.Length != 0)
    //    {
    //        Transform target = rangeChecks[0].transform;
    //        Vector3 directionToTarget = (target.position - enemy.transform.position).normalized;

    //        if (Vector3.Angle(enemy.transform.forward, directionToTarget) < enemy.angle / 2)
    //        {
    //            float distanceToTarget = Vector3.Distance(enemy.transform.position, target.position);

    //            if (!Physics.Raycast(enemy.transform.position, directionToTarget, distanceToTarget, enemy.obstructionMask))
    //            {
    //                enemy.canSeePlayer = true;
    //            }
    //            else
    //            {
    //                enemy.canSeePlayer = false;
    //            }
    //        }
    //        else
    //        {
    //            enemy.canSeePlayer = false;
    //        }
    //    }
    //    else if (enemy.canSeePlayer)
    //    {
    //        enemy.canSeePlayer = false;
    //    }
    //}

    //private IEnumerator FOVRoutine()
    //{
    //    Debug.Log("fovFront");
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        FOVCheck();
    //    }
    //}
}
