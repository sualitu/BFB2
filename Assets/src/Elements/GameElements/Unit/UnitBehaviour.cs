namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Actions.DispatcherActions;
    using BattleForBetelgeuse.Animations;
    using BattleForBetelgeuse.Animations.Combat;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.TweenInteraction;
    using BattleForBetelgeuse.View;

    public class UnitBehaviour : ViewBehaviour<UnitView>, ITweenable {
        private CombatAnimation combatAnimation;

        public HexCoordinate Coordinate { private get; set; }

        public void BeforeTween() {
            new PauseDispatchingAction();
        }

        public void AfterTween() {
            StopThrusters();
            new UnpauseDispatchingAction();
        }

        private void StartThrusters() {
            var thrusters = GetComponentsInChildren<SU_Thruster>();
            foreach (var thruster in thrusters) {
                thruster.StartThruster();
            }
        }

        private void StopThrusters() {
            var thrusters = GetComponentsInChildren<SU_Thruster>();
            foreach (var thruster in thrusters) {
                thruster.StopThruster();
            }
        }

        private void Start() {
            BehaviourUpdater.Behaviours.Add(this);
            gameObject.name = "Unit:" + UniqueId();
            gameObject.tag = "Unit";
            Companion = new UnitView(Coordinate);
            combatAnimation = GetComponentInChildren<CombatAnimation>();
        }

        public void MoveTo(HexCoordinate coordinate) {
            MoveTo(coordinate, Settings.Animations.AnimateMovement);
        }

        public void MoveTo(HexCoordinate coordinate, bool animate) {
            if (!animate) {
                gameObject.transform.position = GridManager.CalculateLocationFromHexCoordinate(coordinate);
            } else {
                StartThrusters();
                Movement.MoveAlongPath(Companion.Path, this);
            }
            CheckCombat();
        }

        private void CheckCombat() {
            if (Companion.AttackTarget != null) {
                new UnitCombatAction(Companion.Coordinate,
                                     Companion.AttackTarget,
                                     UnitStore.Instance.UnitAtTile(Companion.Coordinate),
                                     UnitStore.Instance.UnitAtTile(Companion.AttackTarget));
                Companion.AttackTarget = null;
            }
        }

        public void KillUnit() {
            KillUnit(Settings.Animations.AnimateDeath);
        }

        public void KillUnit(bool animate) {
            if (animate) {
                Explosions.MeshExplosion(gameObject);
                Explosions.TinyExplosion(gameObject);
            }
            Destroy(gameObject);
        }

        public override void PushUpdate() {
            if (Companion.HasMoved) {
                MoveTo(Companion.Coordinate);
            }
            CheckCombat();
            if (Companion.CombatTarget != null) {
                PerformCombat();
            }
        }

        private void PerformCombat() {
            if (Settings.Animations.AnimateCombat) {
                Movement.FaceHex(Companion.CombatTarget, this, "BeginCombat");
            } else {
                CallBack();
            }
        }

        private void BeginCombat() {
            combatAnimation.CombatWith(GridManager.CalculateLocationFromHexCoordinate(Companion.CombatTarget), CallBack);
        }

        private void CallBack() {
            Companion.CombatTarget = null;
            if (!Companion.Alive) {
                KillUnit();
            }
            AfterTween();
        }
    }
}