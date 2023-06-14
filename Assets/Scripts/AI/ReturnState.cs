using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ReturnState : State
{
    public ReturnState(AgentContainer agent) : base(agent) { }

    public override AIState type => AIState.Return;

    private Transform returnPoint;

    public override void Enter()
    {
        returnPoint = GameController.Instance.redBase.transform;
        Debug.Log($"Return Enter {returnPoint}");
    }

    public override void Exit()
    {
        Debug.Log("Return Exit");
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("Return Update");

        Agent.NavAgent.SetDestination(returnPoint.position);
    }
}
