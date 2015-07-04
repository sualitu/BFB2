namespace Assets.Elements.GameElements.Combat.Events {
    public abstract class CombatEvent {
        public CombatEvent(long time) {
            this.Time = time;
        }

        public long Time { get; private set; }
    }
}