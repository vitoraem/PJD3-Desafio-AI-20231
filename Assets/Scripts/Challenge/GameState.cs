using UnityEngine;
namespace ChallengeAI {
  public class GameState
  {
    public ObservableProperty<float> Time = new ObservableProperty<float>(99f);
    public float AmmoRespawnTime { get; protected set; } = 5f;
    public int AmmoReplenish { get; protected set; } = 3;
    public int AmmoInitial { get; protected set; } = 2;
    public float EnergyRespawnTime { get; protected set; } = 5f;
    public float EnergyReplenish { get; protected set; } = 30f;
    public float EnergyPerSecond { get; protected set; } = 8f;
    public float EnergyRefillPerSecond { get; protected set; } = 4f;
    public float EnergyWaitTimeInitial { get; protected set; } = 3f;
    public float EnergyWaitTime { get; protected set; } = 1f;
    public float FireStopTime { get; protected set; } = 1.5f;
    public float FireDamage { get; protected set; } = 30f;
    public float PlayerSpeed { get; protected set; } = 7f;
    public float PlayerAngularSpeed { get; protected set; } = 340f;
    public float PlayerAccelaration { get; protected set; } = 15f;

    // Score on capture enemy flag
    static public int ScoreEnemyFlagCaptureAndDeliver { get; protected set; } = 1000;
    
    // Score on picking up enemy flag
    static public int ScoreEnemyFlagCapture { get; protected set; } = 8;
    
    // Score on picking up dropped flag (on ground)
    static public int ScoreMyFlagCapture { get; protected set; } = 8;
    
    // Score on fire enemy with my flag
    static public int ScoreMyFlagDrop { get; protected set; } = 4;
    
    // Score on picking up ammunition
    static public int ScorePickAmmo { get; protected set; } = 1;
    
    // Score on picking up energy
    static public int ScorePickEnergy { get; protected set; } = 1;


  }
}