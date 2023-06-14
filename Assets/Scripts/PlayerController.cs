using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PlayerController : MonoBehaviour
{
    private Transform tf;

    public FSM Fsm { get; protected set; }
    public AgentContainer Container { get; protected set; }

    public bool HasFlag;

    private void Awake()
    {
        tf = GetComponent<Transform>();

        Container = GetComponent<AgentContainer>();
        Fsm = GetComponent<FSM>();


        Fsm.AddState(new IdleState(Container));
        Fsm.AddState(new WalkState(Container));
        Fsm.AddState(new ChaseState(Container));
        Fsm.AddState(new ReturnState(Container));

        Fsm.frequency = 1f;
    }

    private void Start()
    {
        Fsm.StartFSM();

        Fsm.ChangeState(AIState.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        Flag flag = other.GetComponent<Flag>();
        if(flag)
        {
            HasFlag = true;
            other.transform.parent = tf;
        }
    }
}
