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

        enemy.agent.SetDestination(enemy.player.transform.position);
        enemy.agent.speed *= enemy.chaseMultiplyer;

        enemy.StopHumming();
        enemy.PlayChasing();

        Debug.Log("Chase state");
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.agent.speed = enemy.defaultSpeed;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.agent.SetDestination(enemy.player.transform.position);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.Observing();
    }

    public void ChangeSpeed()
    {
        enemy.agent.speed = enemy.defaultSpeed;
    }
}
