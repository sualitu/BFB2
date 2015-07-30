namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile
{
    using System;

    using Assets.Flux.Actions;
    using Assets.Flux.Stores;

    class MouseStore : PublishingStore<MouseOverStatus>
    {

        private static MouseStore instance;

        private MouseStore()
        {
        }

        public static MouseStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MouseStore();
                }
                return instance;
            }
        }
        private readonly MouseOverStatus status = new MouseOverStatus();

        public static HexCoordinate RecentMouseOver { get; private set; }

        internal override void SendMessage(Message msg) {
            msg(status);
        }

        public override void UpdateStore(Dispatchable action) {
            if (action is MouseOverHexAction) {
                status.PreviousMouseOver = status.CurrentMouseOver;
                status.CurrentMouseOver = ((MouseOverHexAction)action).Coordinate;
                RecentMouseOver = status.CurrentMouseOver;
                Publish();
            }
        }
    }

    public class MouseOverStatus {
        public HexCoordinate CurrentMouseOver { get; set; }
        public HexCoordinate PreviousMouseOver { get; set; }
    }
}
