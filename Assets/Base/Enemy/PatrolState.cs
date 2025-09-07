using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving = false;
    private Vector3 _destionation;

    public void EnterState(Enemy enemy)
    { 
        _isMoving = false;
        enemy.Animator.SetTrigger("Patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!_isMoving)
        {
            _isMoving = true;
            int index = Random.Range(0, enemy.Waypoints.Count);
            _destionation = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destionation;
        }
        else
        {
            if (Vector3.Distance(_destionation, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Patrol State");
    }
}
