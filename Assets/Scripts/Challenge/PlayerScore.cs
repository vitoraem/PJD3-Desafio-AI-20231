namespace ChallengeAI {

  [System.Serializable]
  public class PlayerScore {

    public enum ScoreType {
      None, 
      EnemyFlagCaptureAndDeliver,
      EnemyFlagCapture,
      MyFlagCapture,
      MyFlagDrop,
      PickUpAmmo,
      PickUpEnergy
    }

    public PlayerScore(out System.Action<ScoreType> addScore) {
      addScore = AddScore;
    }

    protected void AddScore(ScoreType type) {
      switch (type)
      {
        case ScoreType.EnemyFlagCaptureAndDeliver:
          CountEnemyFlagCaptureAndDeliver += 1;
          break;
        case ScoreType.EnemyFlagCapture:
          CountEnemyFlagCapture += 1;
          break;
        case ScoreType.MyFlagCapture:
          CountMyFlagCapture += 1;
          break;

        case ScoreType.MyFlagDrop:
          CountMyFlagDrop += 1;
          break;

        case ScoreType.PickUpAmmo:
          CountPickUpAmmo += 1;
          break;

        case ScoreType.PickUpEnergy:
          CountPickUpEnergy += 1;
          break;

        default:
          // No add score
          break;
      }
    }

    public int CountEnemyFlagCaptureAndDeliver { get; protected set; } = 0;
    
    public int CountEnemyFlagCapture { get; protected set; } = 0;
    
    public int CountMyFlagCapture { get; protected set; } = 0;
    
    public int CountMyFlagDrop { get; protected set; } = 0;
    
    public int CountPickUpAmmo { get; protected set; } = 0;
    
    public int CountPickUpEnergy { get; protected set; } = 0;

    public int ScoreTotal {
      get => (
          ScoreEnemyFlagCaptureAndDeliver
        + ScoreEnemyFlagCapture
        + ScoreMyFlagCapture
        + ScoreMyFlagDrop
        + ScorePickUpAmmo
        + ScorePickUpEnergy
      );
    }

    public int ScoreEnemyFlagCaptureAndDeliver {
      get => CountEnemyFlagCaptureAndDeliver * GameState.ScoreEnemyFlagCaptureAndDeliver;
    }
    
    public int ScoreEnemyFlagCapture {
      get => CountEnemyFlagCapture * GameState.ScoreEnemyFlagCapture;
    }
    
    public int ScoreMyFlagCapture {
      get => CountMyFlagCapture * GameState.ScoreMyFlagCapture;
    }
    
    public int ScoreMyFlagDrop {
      get => CountMyFlagDrop * GameState.ScoreMyFlagDrop;
    }
    
    public int ScorePickUpAmmo {
      get => CountPickUpAmmo * GameState.ScorePickAmmo;
    }
    
    public int ScorePickUpEnergy {
      get => CountPickUpEnergy * GameState.ScorePickEnergy;
    }

    public string[,] ScoreToUI() {
      var list = new string[,] {
        {"Enemy Flag Captured and Deliverd",$"{CountEnemyFlagCaptureAndDeliver}",$"{ScoreEnemyFlagCaptureAndDeliver}",},
        {"Enemy Flag Captured",$"{CountEnemyFlagCapture}",$"{ScoreEnemyFlagCapture}",},
        {"My Flag Captured",$"{CountMyFlagCapture}",$"{ScoreMyFlagCapture}",},
        {"My Flag Dropped",$"{CountMyFlagDrop}",$"{ScoreMyFlagDrop}",},
        {"Pick up Ammo",$"{CountPickUpAmmo}",$"{ScorePickUpAmmo}",},
        {"Pick up Energy",$"{CountPickUpEnergy}",$"{ScorePickUpEnergy}",},
      };
      return list;
    }
  }

}