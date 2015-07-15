namespace Assets.Utilities {
    using System;
    using System.Collections.Generic;

    public static class ListExtensions {

        private static readonly Random Rnd = new Random();
        public static List<T> RemoveFirst<T>(this List<T> list) {
            list.RemoveAt(0);
            return list;
        }

        public static T GetFirst<T>(this List<T> list) {
            return list[0];
        }

        public static T GetAndRemoveRandom<T>(this List<T> list) {
            var index = Rnd.Next(0, list.Count-1);
            var removed = list[index];
            list.RemoveAt(index);
            return removed;
        } 
    }
}