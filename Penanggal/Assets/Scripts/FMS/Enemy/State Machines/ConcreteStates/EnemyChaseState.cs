using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.agent = enemy.GetComponent<NavMeshAgent>();

        //enemy.p = enemy.GetComponent<Patrol>();

        //enemy.p.agent.ResetPath();
        enemy.agent.SetDestination(enemy.player.transform.position);

        Debug.Log("chase state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.agent.SetDestination(enemy.player.transform.position);

        ////calculate direction to the player every frame
        //Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;

        ////move player
        ////enemy.MoveEnemy(moveDirection * movementSpeed);

        //if(enemy.IsWithinStrikingDistance)
        //{
        //    enemy.StateMachine.ChangeState(enemy.AttackState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
