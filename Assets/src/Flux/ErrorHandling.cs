namespace Assets.GameManagement {
    using System;

    using UnityEngine;

    public class ErrorHandling {
        public static void InvalidOpration(object subject) {
            Debug.LogError("Invalid Operation on " + subject);
            throw new InvalidOperationException();
        }

        public static void NotImplemented(object subject) {
            Debug.LogError("Not implemented in " + subject);
            throw new NotImplementedException();
        }

        public static void ExceptionInDispatcherThread(Exception e) {
            Debug.Log(String.Format("An exception was thrown in the dispatcher thread: " + e.Message));
            throw e;
        }
    }
}