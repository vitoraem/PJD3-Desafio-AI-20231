using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class IdleState : State
    {
        public IdleState(AgentContainer agent) : base(agent) { }

        public override AIState type { get => AIState.Idle; }

        private float waitSeconds;
        private bool isWaiting;

        IEnumerator LoopWait()
        {
            yield return new WaitForSeconds(waitSeconds);
            isWaiting = false;
        }

        public override void Enter()
        {
            waitSeconds = Random.Range(3f, 8f);
            Debug.Log($"Idle Enter wait {waitSeconds}");
        }

        public override void Exit()
        {
            Debug.Log($"Idle Exit");
        }

        public override void Update(float deltaTime)
        {
            waitSeconds -= deltaTime;
            Debug.Log($"Idle Update {waitSeconds}");

            if (waitSeconds <= 0)
            {
                Agent.fsm.ChangeState(AIState.Chase);
            }
        }
    }

}