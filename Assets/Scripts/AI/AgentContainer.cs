using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentContainer : MonoBehaviour
{
    public Transform Transform { get; protected set; }
    public NavMeshAgent NavAgent { get; protected set; }

    public FSM fsm { get; protected set; }

    public PlayerController Player { get; protected set; }

    private void Awake()
    {
        Transform = GetComponent<Transform>();
        NavAgent = GetComponent<NavMeshAgent>();
        fsm = GetComponent<FSM>();
        Player = GetComponent<PlayerController>();
    }
}
