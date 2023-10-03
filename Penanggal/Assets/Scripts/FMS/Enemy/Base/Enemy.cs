using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }

    #endregion

    #region Idle Variables

    //public float randomMovementRange = 5f;
    //public float randomMovementSpeed = 1f;

    public List<Transform> points = new List<Transform>();
    [HideInInspector]
    public int destPoint = 0;
    [HideInInspector]
    public NavMeshAgent agent;

    #endregion

    #region Chase Variables

    [SerializeField]
    public float lookDistance = 5;
    [SerializeField]
    public LayerMask layerMask;
    [HideInInspector]
    public Vector3 fwd;
    public GameObject player;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        fwd = transform.TransformDirection(Vector3.forward) * lookDistance;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region Health / Die Functions
    
    public void Die()
    {

    }

    #endregion

    #region Distance Checks

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepsSound
    }

    #endregion
}
