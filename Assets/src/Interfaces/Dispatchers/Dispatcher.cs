using System.Collections.Generic;
using System.Threading;
using System.Linq;

using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Actions.DispatcherActions;
using UnityEngine;
using BattleForBetelgeuse.GUI.Board;

namespace BattleForBetelgeuse.Dispatching {

  public class Dispatcher {

    private bool paused = false;
    private static Dispatcher instance;
    Thread dispatcherThread;
    List<IStore> stores;
    private List<Dispatchable> actions;
    private List<Dispatchable> delayedActions;

    public static Dispatcher Instance { 
      get {
        if(instance == null) {
          instance = new Dispatcher();
        }
        return instance;
      } 
    }

    public void Register(IStore store) {
      stores.Add(store);
    }

    private Dispatcher() {
      actions = new List<Dispatchable>();
      delayedActions = new List<Dispatchable>();
      stores = new List<IStore>();
    }

    private void startDispatching() {
      dispatcherThread = new Thread(new ThreadStart(dispatch));
      dispatcherThread.Start();
    }

    private void PauseDispatching() {
      paused = true;
    }

    private void UnpauseDispatching() {
      paused = false;
    }

    public void Signup(Dispatchable action) {
      if(!(action is DispatchingAction)) {
        actions.Add(action);
      } else {
        HandleDispatchingAction((DispatchingAction)action);
      }
      if((dispatcherThread == null || !dispatcherThread.IsAlive)) {
        startDispatching();
      }
    }

    private void HandleDispatchingAction(DispatchingAction dispatchingAction) {
      if(dispatchingAction is PauseDispatchingAction) {
        PauseDispatching();
      } else if(dispatchingAction is UnpauseDispatchingAction) {
        delayedActions.Sort();
        delayedActions.ForEach(action => actions.Add(action));
        delayedActions = new List<Dispatchable>();
        UnpauseDispatching();
        startDispatching();
      }
    }

    private void ThrottleActions(Dispatchable action) {
      if(ThrottledAction.IsThrottled(action)) {
        var actionsCopy = new Dispatchable[actions.Count];
        actions.CopyTo(actionsCopy);
        foreach(var a in actionsCopy) {
          if(!Object.ReferenceEquals(a, action) && a.GetType().Equals(action.GetType())) {
            delayedActions.Add(a);
            actions.Remove(a);
          }
        }
      }
    }

    private void dispatch() {
      while(actions.Count > 0) {
        var action = actions.First();  
        actions.Remove(action);

        try {
          ThrottleActions(action);

          if(!paused || (action is UnpausableAction)) {
            stores.ForEach(store => store.Update(action));
          } else {
            delayedActions.Add(action);
          }
        } catch(System.Exception e) {
          Debug.LogError("Dispatcher crashed: " + e.Message);
          continue;
        }  
      }     
    }
  }
}
