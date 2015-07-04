namespace Assets.Flux.Views {
    using Assets.GameManagement;

    public class BehaviourUpdatingView : IView {
        internal int _id;

        public void SetId(int id) {
            _id = id;
        }

        internal void UpdateBehaviour() {
            BehaviourManager.Updated.Add(_id);
        }
    }
}