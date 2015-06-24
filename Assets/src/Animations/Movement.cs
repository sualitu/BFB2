using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using BattleForBetelgeuse.GUI.Hex;
using System.Collections;
using BattleForBetelgeuse.TweenInteraction;
using BattleForBetelgeuse.GameElements.Units;

namespace BattleForBetelgeuse.Animations {

  public class Movement {
    public static void MoveAlongPath<T>(List<HexCoordinate> path, T tweenableBehaviour) where T : MonoBehaviour, ITweenable {
      tweenableBehaviour.BeforeTween();
      path.Reverse();
      var vectorPath = (from hex in path select GridManager.CalculateLocationFromHexCoordinate(hex)).ToArray();
      var hash = CallBackingTween();
      hash.Add("path", vectorPath);
      hash.Add("orienttopath", true);
      hash.Add("time", path.Count);
      hash.Add("easetype", "easeInOutQuad");

      iTween.MoveTo(tweenableBehaviour.gameObject, hash);
    }

    private static Hashtable CallBackingTween() {      
      var returnTable = new Hashtable();
      returnTable.Add("oncomplete", "AfterTween");
      return returnTable;
    }
  }
}

