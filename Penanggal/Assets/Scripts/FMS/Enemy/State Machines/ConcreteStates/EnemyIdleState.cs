using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyState
{
    private Vector3 targetPos;
    private Vector3 direction;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState() //awake
    {
        base.EnterState();

        //targetPos = GetRandomPointInCircle();

        enemy.agent = enemy.GetComponent<NavMeshAgent>();

        GotoNextPoint();

        Debug.Log("idle state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //if(enemy.IsAggroed)
        //{
        //    enemy.StateMachine.ChangeState(enemy.ChaseState);
        //}

        //direction = (targetPos - enemy.transform.position).normalized;
        ////enemy.MoveEnemy(direction * enemy.randomMovementSpeed);

        ///*if((enemy.transform.position - targetPos).sqrtMagnitude < 0.01f)
        //{
        //    targetPos = GetRandomPointInCircle();
        //}*/

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Physics.Raycast(enemy.transform.position, enemy.fwd, out RaycastHit hit, enemy.lookDistance, enemy.layerMask))
        {
            Debug.Log(hit.collider.gameObject.name + " was hit");
            Debug.DrawRay(enemy.transform.position, enemy.fwd, Color.yellow);
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }

    //private Vector3 GetRandomPointInCircle()
    //{
    //    return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.randomMovementRange;
    //}

    private void GotoNextPoint()
    {
        if (enemy.points.Count == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        enemy.agent.destination = enemy.points[enemy.destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        enemy.destPoint = (enemy.destPoint + 1) % enemy.points.Count;
    }
}
