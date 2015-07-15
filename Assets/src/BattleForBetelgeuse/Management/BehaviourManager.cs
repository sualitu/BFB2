namespace Assets.BattleForBetelgeuse.Management {
    using System;
    using System.Collections.Generic;

    using Assets.Flux.Views;

    using UnityEngine;

    public class BehaviourManager : MonoBehaviour {
        public static List<Guid> Updated;

        public static List<IUpdatableView> Behaviours;


        private void Awake() {
            Updated = new List<Guid>();
            Behaviours = new List<IUpdatableView>();
        }

        private void Update() {
            lock (Updated) {
                var updatedCopy = new Guid[Updated.Count];
                Updated.CopyTo(updatedCopy);
                Updated.Clear();
                foreach (var update in updatedCopy) {
                    var subjectsToChange = Behaviours.FindAll(b => b.UniqueId() == update).GetEnumerator();
                    if (subjectsToChange.MoveNext()) {
                        subjectsToChange.Current.PushUpdate();
                    }
                    while (subjectsToChange.MoveNext()) {
                        subjectsToChange.Current.PushUpdate();
                    }
                }
            }
        }
    }
}