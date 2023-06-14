using UnityEngine;

namespace ChallengeAI
{
  public class Player : IPlayer
  {
    private IPlayerData data;
    public IPlayerData[] enemyData = null;
    private PlayerController controller;
    public Player(IPlayerData dataHandler, PlayerController controller) {
      data = dataHandler;
      this.controller = controller;
    }

    // public float RemainingDistance {get => controller.RemainingDistance;}

    public void SetEnemyData(IPlayerData[] enemyData) {
      // if(enemyData == null) {
        this.enemyData = enemyData;
      //   Debug.Break();
      // }
      Debug.Log($"Player::SetEnemyData {enemyData.Length} '{this.enemyData == null}' {this.enemyData?.Length}");
    }

    public IPlayerData Data => data;
    public IPlayerData[] EnemyData => enemyData;

        // public int PlayerIndex => GameController.Instance.GetPlayerState<int>(this,PlayerStateAttribute.Index);
        // public float Energy => GameController.Instance.GetPlayerState<float>(this,PlayerStateAttribute.Energy);
        // public float Speed => GameController.Instance.GetPlayerState<float>(this,PlayerStateAttribute.Speed);
        // public int Ammo => GameController.Instance.GetPlayerState<int>(this,PlayerStateAttribute.Ammo);
        // public Vector3 FlagPosition => throw new System.NotImplementedException();

        // public Vector3[] PowerUps => throw new System.NotImplementedException();

        // public Vector3[] AmmoRefill => throw new System.NotImplementedException();

        // public bool HasFlag => GameController.Instance.GetPlayerState<bool>(this,PlayerStateAttribute.HasFlag);

        // public float FlagDistance => throw new System.NotImplementedException();

        // public bool IsCooldownFire => GameController.Instance.GetPlayerState<bool>(this,PlayerStateAttribute.IsCooldownFire);

        // public bool HasSightEnemy => GameController.Instance.GetPlayerState<bool>(this,PlayerStateAttribute.HasSightEnemy);

        // public object EnemyInfo => throw new System.NotImplementedException();

    public void Fire()
    {
        controller.Fire();
    }

    public void Move(Vector3 position)
    {
        controller.MoveToDestination(position);
    }

    public void Rotate(float angle)
    {
        controller.Rotate(angle);
    }

    public void Stop()
    {
        controller.Stop();
    }
  }
}