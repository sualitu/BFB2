namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.GUI.Hex;

    public class UnitCombatAction : ThrottledAction {
        public UnitCombatAction(HexCoordinate from, HexCoordinate to, Unit attacker, Unit defender) {
            From = from;
            To = to;
            Attacker = attacker;
            Defender = defender;
            _readyToGo.Set();
        }

        public HexCoordinate From { get; private set; }
        public HexCoordinate To { get; private set; }
        public Unit Attacker { get; private set; }
        public Unit Defender { get; private set; }

        public void Wait() {
            _readyToGo.WaitOne();
        }

        public override string ToString() {
            return string.Format("[UnitCombatAction: from={0}]", From);
        }
    }
}