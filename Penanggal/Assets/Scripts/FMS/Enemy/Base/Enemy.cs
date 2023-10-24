using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    [HideInInspector]
    public NavMeshAgent agent;
    public FieldOfView fovFront, fovBack;
    public float lookDelay = 5f;
    [SerializeField]
    private float changeStateDelay = 5f;

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyPatrolState PatrolState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }

    #endregion

    #region Idle Variables

    public List<Transform> points = new List<Transform>();
    [HideInInspector]
    public int destPoint = 0;
    public float yRotation;

    #endregion

    #region Patrol Variables

    #endregion

    #region Chase Variables

    public GameObject player;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        PatrolState = new EnemyPatrolState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        //fwd = transform.TransformDirection(Vector3.forward) * lookDistance;
        //Debug.DrawRay(transform.position, fwd, Color.green);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void Observing()
    {
        if (fovFront.canSeePlayer)
        {
            if (StateMachine.CurrentEnemyState == ChaseState)
            {
                return;
            }
            else
            {
                Invoke("ChangeStateChase", 0.1f);
            }
        }
        else if (!fovFront.canSeePlayer)
        {
            if (StateMachine.CurrentEnemyState == IdleState)
            {
                return;
            }
            else
            {
                Invoke("ChangeStateIdle", changeStateDelay);
            }
        }

        if (fovBack.canSeePlayer)
        {
            if (StateMachine.CurrentEnemyState == ChaseState)
            {
                return;
            }
            else
            {
                Invoke("ChangeStateChase", Random.Range(0.5f, 1f)); 
            }
        }
        else if (!fovBack.canSeePlayer)
        {
            if (StateMachine.CurrentEnemyState == IdleState)
            {
                return;
            }
            else
            {
                Invoke("ChangeStateIdle", changeStateDelay);
            }
        }
    }

    public void ChangeStateIdle()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void ChangeStateChase()
    {
        StateMachine.ChangeState(ChaseState);
    }

    public void ChangeStatePatrol()
    {
        StateMachine.ChangeState(PatrolState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
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
