using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WalkState : State
    {
        public WalkState(AgentContainer agent) : base(agent) { }

        public override AIState type => AIState.Walk;

        public float Speed;

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }
    }

}