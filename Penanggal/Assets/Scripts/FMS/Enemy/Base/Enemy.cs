using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    public GameObject penanggal;

    [HideInInspector]
    public NavMeshAgent agent;
    public FieldOfView fovFront, fovBack;
    public float lookDelay = 5f;
    [SerializeField]
    private float changeStateDelay = 5f;

    [HideInInspector]
    public float defaultSpeed;

    private AudioSource audioSource;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public ParticleSystem blood;

    public Camera aiCam;
    public Camera playerCam;
    [HideInInspector] public bool playerDied = false;

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

    [HideInInspector]
    public bool isRouteComplete = false;
    [HideInInspector]
    public int pointsPatroled = 0;

    public List<Transform> TPpoints = new List<Transform>();

    #endregion

    #region Chase Variables

    public GameObject player;
    public float chaseMultiplyer = 1.2f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        PatrolState = new EnemyPatrolState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    private IEnumerator Start()
    {
        defaultSpeed = agent.speed;
        StateMachine.Initialize(IdleState);
        yield return new WaitForEndOfFrame();
        agent.enabled = true;
        agent.enabled = false;
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
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
                Invoke("ChangeStateChase", Random.Range(0.1f, 0.5f)); 
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

    public void PlayHumming()
    {
        audioSource.Play();
    }

    public void StopHumming()
    {
        audioSource.Stop();
    }

    public void PlayChasing()
    {
        FindObjectOfType<AudioManager>().PlaySFX("PenanggalChasing");
    }

    public void StopChasing()
    {
        FindObjectOfType<AudioManager>().StopSFX("PenanggalChasing");
    }

    public void StartAnim()
    {
        Debug.Log("Playing Attack Anim - Penanggal");
        animator.SetBool("isAttacking", true);
    }

    public void StopAnim()
    {
        Debug.Log("Stopping Attack Anim - Penanggal");
        animator.SetBool("isAttacking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Playing Attack Anim - Penanggal");

            if(!playerDied)
            {
                playerCam.enabled = false;
                aiCam.enabled = true;
                playerDied = true;
            }
            Debug.Log("ai cam is on");
            

            agent.speed = 0;

            StopChasing();
            FindObjectOfType<AudioManager>().PlaySFX("PenaggalDeath");
            StartAnim();
        }
    }

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
