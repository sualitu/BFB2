using System.Collections.Generic;

namespace ExtensionMethods {

  public static class ListExtensions {
    public static List<T> RemoveFirst<T>(this List<T> list) {
      list.RemoveAt(0);
      return list;
    }

    public static T GetFirst<T>(this List<T> list) {
      return list[0];
    }
  }
}

