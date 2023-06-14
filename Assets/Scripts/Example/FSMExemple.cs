using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChallengeAI;

namespace Raguze
{


    public class BaseState
    {
        public const string CAPTURE_FLAG = "CaptureFlagState";
        public const string RETURN_FLAG = "ReturnFlagState";
        public const string PICKUP_ENERGY = "PickupEnergyState";
        public const string WAIT_ENERGY = "WaitEnergy";
    }

    public class FSMInitExemple : FSMInitializer
    {
        public override string Name => "AI Exemple";

        public override void Init()
        {
            RegisterState<CaptureFlagState>(BaseState.CAPTURE_FLAG);
            RegisterState<ReturnFlagState>(BaseState.RETURN_FLAG);
            RegisterState<PickupEnergyState>(BaseState.PICKUP_ENERGY);
            RegisterState<WaitEnergy>(BaseState.WAIT_ENERGY);
        }
    }

    public class CaptureFlagState : State
    {
        public CaptureFlagState(string name, IPlayer player, FSMChangeState changeStateDelegate) : base(name,player,changeStateDelegate) { }

        public Vector3 Destination;

        public override void Enter()
        {
            Destination = Agent.EnemyData[0].FlagPosition ?? Vector3.zero;
            Agent.Move(Destination);
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if(Agent.Data.HasFlag)
            {
                ChangeState(BaseState.RETURN_FLAG);
            }
        }
    }

    public class ReturnFlagState : State
    {
        public ReturnFlagState(string name, IPlayer player, FSMChangeState changeStateDelegate) : base(name, player, changeStateDelegate) { }

        Vector3 Destination;

        public override void Enter()
        {
            Destination = Agent.Data.StartPosition;
            Agent.Move(Destination);
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if(Agent.Data.Energy < 25f)
            {
                ChangeState(BaseState.PICKUP_ENERGY);
            }
        }
    }

    public class PickupEnergyState : State
    {
        public PickupEnergyState(string name, IPlayer player, FSMChangeState changeStateDelegate) : base(name, player, changeStateDelegate) { }

        Vector3 Destination;

        public override void Enter()
        {
            if(Agent.Data.PowerUps.Length > 0)
            {
                Destination = Agent.Data.PowerUps[0];
                Agent.Move(Destination);
            }
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if(Agent.Data.Energy < 10f)
            {
                ChangeState(BaseState.WAIT_ENERGY);
                return;
            }

            if(Agent.Data.RemainingDistance <= 0.01f)
            {
                if(Agent.Data.HasFlag)
                {
                    ChangeState(BaseState.RETURN_FLAG);
                }
                else
                {
                    ChangeState(BaseState.CAPTURE_FLAG);
                }
            }
        }
    }

    public class WaitEnergy : State
    {
        public WaitEnergy(string name, IPlayer player, FSMChangeState changeStateDelegate) : base(name, player, changeStateDelegate) { }

        public override void Enter()
        {
            Agent.Stop();
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if(Agent.Data.Energy > 50f)
            {
                if(Agent.Data.HasFlag)
                {
                    ChangeState(BaseState.RETURN_FLAG);
                }
            }
        }
    }
}
