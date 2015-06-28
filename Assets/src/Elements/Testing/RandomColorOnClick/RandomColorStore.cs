using Rnd = System.Random;

namespace BattleForBetelgeuse.Stores {
    using BattleForBetelgeuse.Actions;

    using UnityEngine;

    public class RandomColorStore : PublishingStore<Color> {
        private static RandomColorStore instance;

        private readonly Rnd rnd;

        private Color color;

        private RandomColorStore() {
            this.color = Color.black;
            this.rnd = new Rnd();
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
            msg(this.color);
        }

        public override void Update(Dispatchable action) {
            if (action is RandomColorOnClickAction) {
                this.color = new Color((float)this.rnd.NextDouble(),
                                       (float)this.rnd.NextDouble(),
                                       (float)this.rnd.NextDouble());
                this.Publish();
            }
        }
    }
}