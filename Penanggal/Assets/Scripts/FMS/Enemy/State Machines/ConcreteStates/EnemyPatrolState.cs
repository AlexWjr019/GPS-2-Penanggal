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
            enemy.StateMachine.ChangeState(enemy.IdleState);
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

    private void GotoNextPoint()
    {
        if (enemy.points.Count == 0)
        {
            return;
        }

        enemy.agent.destination = enemy.points[enemy.destPoint].position;

        enemy.destPoint = (enemy.destPoint + 1) % enemy.points.Count;
    }
}
