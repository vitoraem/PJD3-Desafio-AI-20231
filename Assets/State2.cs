﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Added since we're using a navmesh.

public class State2
{
    // 'States' that the NPC could be in.
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
    };

    // 'Events' - where we are in the running of a STATE.
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; // To store the name of the STATE.
    protected EVENT stage; // To store the stage the EVENT is in.
    protected GameObject npc; // To store the NPC game object.
    protected Animator anim; // To store the Animator component.
    protected Transform player; // To store the transform of the player. This will let the guard know where the player is, so it can face the player and know whether it should be shooting or chasing (depending on the distance).
    protected State2 nextState; // This is NOT the enum above, it's the state that gets to run after the one currently running (so if IDLE was then going to PATROL, nextState would be PATROL).
    protected NavMeshAgent agent; // To store the NPC NavMeshAgent component.

    float visDist = 10.0f; // When the player is within a distance of 10 from the NPC, then the NPC should be able to see it...
    float visAngle = 30.0f; // ...if the player is within 30 degrees of the line of sight.
    float shootDist = 7.0f; // When the player is within a distance of 7 from the NPC, then the NPC can go into an ATTACK state.

    // Constructor for State
    public State2(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    // Phases as you go through the state.
    public virtual void Enter() { stage = EVENT.UPDATE; } // Runs first whenever you come into a state and sets the stage to whatever is next, so it will know later on in the process where it's going.
    public virtual void Update() { stage = EVENT.UPDATE; } // Once you are in UPDATE, you want to stay in UPDATE until it throws you out.
    public virtual void Exit() { stage = EVENT.EXIT; } // Uses EXIT so it knows what to run and clean up after itself.

    // The method that will get run from outside and progress the state through each of the different stages.
    public State2 Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState; // Notice that this method returns a 'state'.
        }
        return this; // If we're not returning the nextState, then return the same state.
    }

    // Can the NPC see the player, using a simple Line Of Sight calculation?
    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position; // Provides the vector from the NPC to the player.
        float angle = Vector3.Angle(direction, npc.transform.forward); // Provide angle of sight.

        // If player is close enough to the NPC AND within the visible viewing angle...
        if(direction.magnitude < visDist && angle < visAngle)
        {
            return true; // NPC CAN see the player.
        }
        return false; // NPC CANNOT see the player.
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position; // Provides the vector from the player to the NPC.
        float angle = Vector3.Angle(direction, npc.transform.forward); // Provide angle of sight.

        // If player is close enough to the NPC AND within the visible viewing angle...
        if (direction.magnitude < 2 && angle < 30)
        {
            return true; // Player IS behind the NPC.
        }
        return false; // Player IS NOT behind the NPC.
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position; // Provides the vector from the NPC to the player.
        if(direction.magnitude < shootDist)
        {
            return true; // NPC IS close enough to the player to attack.
        }
        return false; // NPC IS NOT close enough to the player to attack.
    }
}

// Constructor for Idle state.
public class Idle : State2
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE; // Set name of current state.
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle"); // Sets any current animation state back to Idle.
        base.Enter(); // Sets stage to UPDATE.
    }
    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT; // The next time 'Process' runs, the EXIT stage will run instead, which will then return the nextState.
        }
        // The only place where Update can break out of itself. Set chance of breaking out at 10%.
        else if(Random.Range(0,100) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT; // The next time 'Process' runs, the EXIT stage will run instead, which will then return the nextState.
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle"); // Makes sure that any events queued up for Idle are cleared out.
        base.Exit();
    }
}

// Constructor for Patrol state.
public class Patrol : State2
{
    int currentIndex = -1;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL; // Set name of current state.
        agent.speed = 2; // How fast your character moves ONLY if it has a path. Not used in Idle state since agent is stationary.
        agent.isStopped = false; // Start and stop agent on current path using this bool.
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity; // Store distance between NPC and waypoints.

        // Calculate closest waypoint by looping around each one and calculating the distance between the NPC and each waypoint.
        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if(distance < lastDist)
            {
                currentIndex = i - 1; // Need to subtract 1 because in Update, we add 1 to i before setting the destination.
                lastDist = distance;
            }
        }
        anim.SetTrigger("isWalking"); // Start agent walking animation.
        base.Enter();
    }

    public override void Update()
    {
        // Check if agent hasn't finished walking between waypoints.
        if(agent.remainingDistance < 1)
        {
            // If agent has reached end of waypoint list, go back to the first one, otherwise move to the next one.
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position); // Set agents destination to position of next waypoint.
        }

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT; // The next time 'Process' runs, the EXIT stage will run instead, which will then return the nextState.
        }

        else if (IsPlayerBehind())
        {
            nextState = new RunAway(npc, agent, anim, player);
            stage = EVENT.EXIT; // The next time 'Process' runs, the EXIT stage will run instead, which will then return the nextState.
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isWalking"); // Makes sure that any events queued up for Walking are cleared out.
        base.Exit();
    }
}

public class Pursue : State2
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE; // State set to match what NPC is doing.
        agent.speed = 5; // Speed set to make sure NPC appears to be running.
        agent.isStopped = false; // Set bool to determine NPC is moving.
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning"); // Set running trigger to change animation.
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);  // Set goal for NPC to reach but navmesh processing might not have taken place, so...
        if(agent.hasPath)                       // ...check if agent has a path yet.
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player); // If NPC can attack player, set correct nextState.
                stage = EVENT.EXIT; // Set stage correctly as we are finished with Pursue state.
            }
            // If NPC can't see the player, switch back to Patrol state.
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player); // If NPC can't see player, set correct nextState.
                stage = EVENT.EXIT; // Set stage correctly as we are finished with Pursue state.
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning"); // Makes sure that any events queued up for Running are cleared out.
        base.Exit();
    }
}

public class Attack : State2
{
    float rotationSpeed = 2.0f; // Set speed that NPC will rotate around to face player.
    AudioSource shoot; // To store the AudioSource component.
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK; // Set name to correct state.
        shoot = _npc.GetComponent<AudioSource>(); // Get AudioSource component for shooting sound.
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting"); // Set shooting trigger to change animation.
        agent.isStopped = true; // Stop NPC so he can shoot.
        shoot.Play(); // Play shooting sound.
        base.Enter();
    }

    public override void Update()
    {
        // Calculate direction and angle to player.
        Vector3 direction = player.position - npc.transform.position; // Provides the vector from the NPC to the player.
        float angle = Vector3.Angle(direction, npc.transform.forward); // Provide angle of sight.
        direction.y = 0; // Prevent character from tilting.

        // Rotate NPC to always face the player that he's attacking.
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                            Quaternion.LookRotation(direction),
                                            Time.deltaTime * rotationSpeed);

        if(!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player); // If NPC can't attack player, set correct nextState.
            stage = EVENT.EXIT; // Set stage correctly as we are finished with Attack state.
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isShooting"); // Makes sure that any events queued up for Shooting are cleared out.
        shoot.Stop(); // Stop shooting sound.
        base.Exit();
    }
}

public class RunAway : State2
{
    GameObject safeLocation; // Store object used for safe location.
    
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY; // Set name to correct state.
        safeLocation = GameObject.FindGameObjectWithTag("Safe"); // Find object that was tagged with "Safe" and assign top safeLocation.
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning"); // Set running trigger to change animation.
        agent.isStopped = false; // Set bool to determine NPC is moving.
        agent.speed = 6; // Set speed slightly fsater than when running towards player.
        agent.SetDestination(safeLocation.transform.position); // Set goal for agent to be the safe location.
        base.Enter();
    }

    public override void Update()
    {
        // When the NPC hits the top of the cube, return to the Idle state that has a 10% chance of going into Patrol state.
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player); // If NPC can't attack player, set correct nextState.
            stage = EVENT.EXIT; // Set stage correctly as we are finished with Runaway state.
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning"); // Makes sure that any events queued up for Running are cleared out.
        base.Exit();
    }
}