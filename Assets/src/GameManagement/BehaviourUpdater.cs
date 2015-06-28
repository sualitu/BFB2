namespace BattleForBetelgeuse {
    using System.Collections.Generic;

    using BattleForBetelgeuse.View;

    using UnityEngine;

    public class BehaviourUpdater : MonoBehaviour {
        public static List<int> Updated = new List<int>();

        public static List<UpdatableView> Behaviours = new List<UpdatableView>();

        public static Prefabs Prefabs { get; private set; }

        private void Awake() {
            Prefabs = GetComponent<Prefabs>();
            GridManager.Init();
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