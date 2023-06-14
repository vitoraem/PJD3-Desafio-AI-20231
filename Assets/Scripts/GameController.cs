using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class GameController : MonoBehaviour
{
    public FSM fsm;
    static public GameController Instance { get; protected set; }

    public List<Checkpoint> checkpoints = new List<Checkpoint>();

    public PlayerController player;

    public Checkpoint startPoint;
    public Checkpoint endPoint;
    public Checkpoint redBase;
    public Checkpoint redFlag;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();

        checkpoints.AddRange(FindObjectsOfType<Checkpoint>());

        startPoint = checkpoints.Find((c) => 
        {
            return c.type == CheckpointType.Start;
        });

        endPoint = checkpoints.Find((c) =>
        {
            return c.type == CheckpointType.End;
        });

        redBase = checkpoints.Find((c) =>
        {
            return c.type == CheckpointType.RedBase;
        });

        redFlag = checkpoints.Find((c) =>
        {
            return c.type == CheckpointType.RedFlag;
        });

        float playerY = player.transform.position.y;
        player.transform.position = new Vector3(
            redBase.transform.position.x,
            playerY,
            redBase.transform.position.z
        );
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
