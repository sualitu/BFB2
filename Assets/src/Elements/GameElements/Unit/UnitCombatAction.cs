namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.GUI.Hex;

    public class UnitCombatAction : Dispatchable {
        public UnitCombatAction(HexCoordinate from, HexCoordinate to, Unit attacker, Unit defender) {
            this.From = from;
            this.To = to;
            this.Attacker = attacker;
            this.Defender = defender;
            this._readyToGo.Set();
        }

        public HexCoordinate From { get; private set; }
        public HexCoordinate To { get; private set; }
        public Unit Attacker { get; private set; }
        public Unit Defender { get; private set; }

        public void Wait() {
            this._readyToGo.WaitOne();
        }

        public override string ToString() {
            return string.Format("[UnitCombatAction: from={0}]", this.From);
        }
    }
}