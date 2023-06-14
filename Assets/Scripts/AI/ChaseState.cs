using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI;

public class ChaseState : State
{
    public override AIState type => AIState.Chase;

    public ChaseState(AgentContainer agent) : base(agent) { }

    private Transform target;

    public override void Enter()
    {
        target = GameController.Instance.redFlag.transform;
        Debug.Log($"Chase Enter {target}");
    }

    public override void Exit()
    {
        Debug.Log($"Chase Exit");
    }

    public override void Update(float deltaTime)
    {
        Debug.Log($"Chase Update");

        Agent.NavAgent.SetDestination(target.position);

        if(Agent.Player.HasFlag)
        {
            Agent.fsm.ChangeState(AIState.Return);
        }

    }
}
