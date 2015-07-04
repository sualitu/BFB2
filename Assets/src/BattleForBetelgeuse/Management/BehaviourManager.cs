namespace Assets.BattleForBetelgeuse.Management {
    using System.Collections.Generic;

    using Assets.Flux.Views;

    using UnityEngine;

    public class BehaviourManager : MonoBehaviour {
        public static List<int> Updated = new List<int>();

        public static List<IUpdatableView> Behaviours = new List<IUpdatableView>();


        private void Awake() {
        }

        private void Update() {
            lock (Updated) {
                var updatedCopy = new int[Updated.Count];
                Updated.CopyTo(updatedCopy);
                Updated.Clear();
                foreach (var update in updatedCopy) {
                    var subjectsToChange = Behaviours.FindAll(b => b.UniqueId() == update).GetEnumerator();
                    while (subjectsToChange.MoveNext()) {
                        subjectsToChange.Current.PushUpdate();
                    }
                }
            }
        }
    }
}