using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (!enemy.isRouteComplete)
        {
            enemy.agent.destination = enemy.points[enemy.destPoint].position;
            enemy.destPoint = (enemy.destPoint + 1) % enemy.points.Count;
            enemy.pointsPatroled++;

            if (enemy.pointsPatroled > enemy.points.Count)
            {
                enemy.pointsPatroled = 0;
                enemy.isRouteComplete = true;
            }
        }
        else if (enemy.isRouteComplete && !(enemy.StateMachine.CurrentEnemyState == enemy.ChaseState))
        {
            int pos = Random.Range(0, 3);

            switch (pos)
            {
                case 0:
                    Debug.Log("TP penanggal to Kitchen");
                    enemy.penanggal.transform.position = enemy.TPpoints[pos].position;
                    enemy.isRouteComplete = false;
                    break;
                case 1:
                    Debug.Log("TP penanggal to Bedroom");
                    enemy.penanggal.transform.position = enemy.TPpoints[pos].position;
                    enemy.isRouteComplete = false;
                    break;
                case 2:
                    Debug.Log("TP penanggal to Bathroom");
                    enemy.penanggal.transform.position = enemy.TPpoints[pos].position;
                    enemy.isRouteComplete = false;
                    break;
                case 3:
                    Debug.Log("TP penanggal to Living Room");
                    enemy.penanggal.transform.position = enemy.TPpoints[pos].position;
                    enemy.isRouteComplete = false;
                    break;
            }
        }  
    }
}
