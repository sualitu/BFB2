using Rnd = System.Random;

namespace Assets.Elements.Playground.RandomColorOnClick {
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;

    using UnityEngine;

    public class RandomColorStore : PublishingStore<Color> {
        private static RandomColorStore instance;

        private readonly Rnd rnd;

        private Color color;

        private RandomColorStore() {
            color = Color.black;
            rnd = new Rnd();
        }

        public static RandomColorStore Instance {
            get {
                if (instance == null) {
                    instance = new RandomColorStore();
                }
                return instance;
            }
        }

        internal override void SendMessage(Message msg) {
            msg(color);
        }

        public override void Update(Dispatchable action) {
            if (action is RandomColorOnClickAction) {
                color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
                Publish();
            }
        }
    }
}