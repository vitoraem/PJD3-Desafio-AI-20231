using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI
{

    public enum AIState
    {
        None,
        Idle,
        Walk,
        Chase,
        Return
    }

    public abstract class State
    {
        public AgentContainer Agent { get; protected set; }

        public virtual AIState type { get; }

        public State(AgentContainer agent)
        {
            Agent = agent;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update(float deltaTime);

    }

}

