using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public Player Player;
    [SerializeField]
    public float PatrolSpeed;
    [SerializeField]
    public float ChaseSpeed;

    public BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    [HideInInspector]
    public Animator Animator;

    private void OnDestroy()
    {
        if (Player != null)
        {
            Player.onPowerUpStart -= StartRetreating;
            Player.onPowerUpStop -= StopRetreating;
        }
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
    }

    private void Start()
    {
        if (Player != null)
        {
            Player.onPowerUpStart += StartRetreating;
            Player.onPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if(_currentState != null)
        {
            _currentState.UpdateState(this);
        }
        Animator.SetFloat("Velocity", NavMeshAgent.velocity.magnitude);
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        if (_currentState == RetreatState)
        {
            SwitchState(PatrolState);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (_currentState != RetreatState)
    //    {
    //        if (collision.gameObject.CompareTag("Player"))
    //        {
    //            collision.gameObject.GetComponent<Player>().Dead();
    //        }
    //    }
    //}
}
