namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Actions.DispatcherActions;
    using BattleForBetelgeuse.Animations;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.TweenInteraction;
    using BattleForBetelgeuse.View;

    public class UnitBehaviour : ViewBehaviour<UnitView>, ITweenable {
        public HexCoordinate Coordinate { private get; set; }

        public void BeforeTween() {
            new PauseDispatchingAction();
        }

        public void AfterTween() {
            new UnpauseDispatchingAction();
        }

        private void Start() {
            BehaviourUpdater.Behaviours.Add(this);
            this.gameObject.name = "Unit:" + this.UniqueId();
            this.gameObject.tag = "Unit";
            this.Companion = new UnitView(this.Coordinate);
        }

        public void MoveTo(HexCoordinate coordinate) {
            this.MoveTo(coordinate, Settings.Animations.AnimateMovement);
        }

        public void MoveTo(HexCoordinate coordinate, bool animation) {
            if (!animation) {
                this.gameObject.transform.position = GridManager.CalculateLocationFromHexCoordinate(coordinate);
            } else {
                Movement.MoveAlongPath(this.Companion.Path, this);
            }
            this.CheckCombat();
        }

        private void CheckCombat() {
            if (this.Companion.AttackTarget != null) {
                new UnitCombatAction(this.Companion.Coordinate,
                                     this.Companion.AttackTarget,
                                     UnitStore.Instance.UnitAtTile(this.Companion.Coordinate),
                                     UnitStore.Instance.UnitAtTile(this.Companion.AttackTarget));
                this.Companion.AttackTarget = null;
            }
        }

        public void KillUnit() {
            this.KillUnit(Settings.Animations.AnimateDeath);
        }

        public void KillUnit(bool animation) {
            if (animation) {
                Explosions.MeshExplosion(this.gameObject);
                Explosions.TinyExplosion(this.gameObject);
            }
            Destroy(this.gameObject);
        }

        public override void PushUpdate() {
            if (this.Companion.HasMoved) {
                this.MoveTo(this.Companion.Coordinate);
            }
            if (!this.Companion.Alive) {
                this.KillUnit();
            }
            this.CheckCombat();
        }
    }
}