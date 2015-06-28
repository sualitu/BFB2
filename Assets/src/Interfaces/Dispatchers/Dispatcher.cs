namespace BattleForBetelgeuse.Dispatching {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.Actions.DispatcherActions;
    using BattleForBetelgeuse.Stores;

    using UnityEngine;

    public class Dispatcher {
        private static Dispatcher instance;

        private readonly List<Dispatchable> actions;

        private readonly List<IStore> stores;

        private List<Dispatchable> delayedActions;

        private Thread dispatcherThread;

        private bool paused;

        private Dispatcher() {
            actions = new List<Dispatchable>();
            delayedActions = new List<Dispatchable>();
            stores = new List<IStore>();
        }

        public static Dispatcher Instance {
            get {
                if (instance == null) {
                    instance = new Dispatcher();
                }
                return instance;
            }
        }

        public void Register(IStore store) {
            stores.Add(store);
        }

        private void startDispatching() {
            dispatcherThread = new Thread(Dispatch);
            dispatcherThread.Start();
        }

        private void PauseDispatching() {
            paused = true;
        }

        private void UnpauseDispatching() {
            paused = false;
        }

        public void Signup(Dispatchable action) {
            if (!(action is DispatchingAction)) {
                actions.Add(action);
            } else {
                HandleDispatchingAction((DispatchingAction)action);
            }
            if ((dispatcherThread == null || !dispatcherThread.IsAlive)) {
                startDispatching();
            }
        }

        private void HandleDispatchingAction(DispatchingAction dispatchingAction) {
            if (dispatchingAction is PauseDispatchingAction) {
                PauseDispatching();
            } else if (dispatchingAction is UnpauseDispatchingAction) {
                delayedActions.Sort();
                delayedActions.ForEach(action => actions.Add(action));
                delayedActions = new List<Dispatchable>();
                UnpauseDispatching();
                startDispatching();
            }
        }

        private void ThrottleActions(Dispatchable action) {
            if (ThrottledAction.IsThrottled(action)) {
                var actionsCopy = new Dispatchable[actions.Count];
                actions.CopyTo(actionsCopy);
                foreach (var a in actionsCopy) {
                    if (!ReferenceEquals(a, action) && ThrottledAction.IsThrottled(a)) {
                        delayedActions.Add(a);
                        actions.Remove(a);
                    }
                }
            }
        }

        private void Dispatch() {
            while (actions.Count > 0) {
                var action = actions.First();
                actions.Remove(action);

                try {
                    ThrottleActions(action);

                    if (!paused || (action is UnpausableAction)) {
                        stores.ForEach(store => store.Update(action));
                    } else {
                        delayedActions.Add(action);
                    }
                } catch (Exception e) {
                    Debug.LogError("An error occured in the dispatcher thread: " + e.Message);
                }
            }
        }
    }
}