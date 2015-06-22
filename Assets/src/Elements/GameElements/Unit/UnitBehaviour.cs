using BattleForBetelgeuse.View;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Animations;
using BattleForBetelgeuse.TweenInteraction;
using BattleForBetelgeuse.Actions.DispatcherActions;
using UnityEngine;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class UnitBehaviour : ViewBehaviour<UnitView>, ITweenable {

    public HexCoordinate Coordinate { private get; set; }

    void Start() {
      BehaviourUpdater.Behaviours.Add(this);
      gameObject.name = "Unit:" + UniqueId();
      gameObject.tag = "Unit";
      Companion = new UnitView(Coordinate);
    }

    public void MoveTo(HexCoordinate coordinate) { MoveTo(coordinate,  Settings.Animations.AnimateMovement); }

    public void MoveTo(HexCoordinate coordinate, bool animation) {
      if(!animation) {        
        gameObject.transform.position = GridManager.CalculateLocationFromHexCoordinate(coordinate);
      } else {
        Animations.Movement.MoveAlongPath<UnitBehaviour>(Companion.Path, this);
      }
      if(Companion.AttackTarget != null) {
        new UnitCombatAction(Companion.Coordinate, UnitStore.Instance.UnitAtTile(Companion.Coordinate), UnitStore.Instance.UnitAtTile(Companion.AttackTarget));
      }
    }

    public void KillUnit() { KillUnit(Settings.Animations.AnimateDeath); }

    public void KillUnit(bool animation) { 
      if(animation) {
        Explosions.MeshExplosion(gameObject);
        Explosions.TinyExplosion(gameObject);
      }
      Destroy(gameObject);
    }

    public override void PushUpdate() {
      if(Companion.HasMoved) {
        MoveTo(Companion.Coordinate);
      }
      if(!Companion.Alive) { 
        KillUnit();
      }
    }

    public void BeforeTween() {
      new PauseDispatchingAction();
    }

    public void AfterTween() {
      new UnpauseDispatchingAction();
    }
  }
}

