using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        enemy.NavMeshAgent.speed = enemy.ChaseSpeed;
    }
    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Patrol State");
    }
}
