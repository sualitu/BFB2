namespace BattleForBetelgeuse {

  public class ErrorHandling {
    public static void InvalidOpration(object subject) {
      UnityEngine.Debug.LogError("Invalid Operation on " + subject); 
      throw new System.InvalidOperationException(); 
    }

    public static void NotImplemented(object subject) {
      UnityEngine.Debug.LogError("Not implemented in " + subject); 
      throw new System.NotImplementedException(); 
    }
  }
}

