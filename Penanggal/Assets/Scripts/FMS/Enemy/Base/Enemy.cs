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
    [HideInInspector]
    public FieldOfView fov;
    public float lookTimer = 5f;

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
    public float x, y, z;

    #endregion

    #region Patrol Variables

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
        PatrolState = new EnemyPatrolState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
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
        if (fov.canSeePlayer)
        {
            StateMachine.ChangeState(ChaseState);
        }
        else if (!fov.canSeePlayer)
        {
            StateMachine.ChangeState(IdleState);
        }

        //if (Physics.Raycast(enemy.transform.position, enemy.fwd, out RaycastHit hit, enemy.lookDistance, enemy.layerMask))
        //{
        //    if (hit.collider.name == "Player")
        //    {
        //        Debug.Log(hit.collider.gameObject.name + " was hit");
        //        Debug.DrawRay(enemy.transform.position, enemy.fwd, Color.yellow);
        //        enemy.StateMachine.ChangeState(enemy.ChaseState);
        //    }
        //}
    }

    //public IEnumerator LookArd()
    //{
    //    transform.localEulerAngles = new Vector3(transform.rotation.x + 20, 0, 0);
    //    yield return new WaitForSeconds(lookTimer / 2);

    //    transform.localEulerAngles = new Vector3(transform.rotation.x - 20, 0, 0);
    //    yield return new WaitForSeconds(lookTimer / 2);
    //}

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
