using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ChallengeAI {
  [Serializable]
  public class FSMInitializer : IFSMInitializer {
    protected List<IStateInfo> stateInfos = new List<IStateInfo>();

    public virtual string Name {get => "AI";}
    public State[] GetStates(IPlayer player, FSMChangeState changeStateDelegate) {
      Init();
      List<State> states = new List<State>();
      stateInfos.ForEach(s => {
        Debug.Log($">> {s.Name} {s.Type}");
        var ctors = s.Type.GetConstructors();
        var state = ctors[0].Invoke(new object[]{s.Name,player,changeStateDelegate});
        states.Add((State)state);
      });
      return states.ToArray();
      // return new State[] {
        
      //   new WalkTestState(ExampleState.WALK,player,changeStateDelegate),
      // };
    }

    public void RegisterState<T>(string name) where T : State {
      var state = new StateInfo<T>(name);
      Debug.Log($"Register State {name} t:{state.Type} as:{state as StateInfo<State>}");
      stateInfos.Add(state);
    }

    public virtual void Init() {
      var states = new System.Type[] {
        typeof(IdleTestState)
      };
      var si = new StateInfo<IdleTestState>("idle");
      var ctrs = si.Type.GetConstructors();
      
    }
  }

  public interface IStateInfo {
    public string Name {get; set;}
    public System.Type Type {get; set;}
  }

  public class StateInfo<T> : IStateInfo where T : State {
    // public System.Type Type;
    // public string Name;
    public StateInfo(string name) {
      Type = typeof(T);
      Name = name;
    }

    public string Name { get; set; }
    public Type Type { get; set; }
  }
}