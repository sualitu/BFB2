namespace Assets.Utilities {
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    public class Logger : MonoBehaviour {
        private static List<string> log;

        private string fileName;

        public static void Log(string message) {
            log.Add(message);
        }

        private void Start() {
            log = new List<string>();
            fileName = Application.dataPath + "logs/DebugLog.txt";
        }

        private void Update() {
            var lines = new string[log.Count];
            Array.Copy(log.ToArray(), lines, log.Count);
            log.Clear();
#if UNITY_EDITOR
            foreach (var line in lines) {
                Debug.Log(line);
            }
#else
            using (var file = new StreamWriter(fileName)) {
                foreach (var line in lines) {
                    file.WriteLine(line);
                }
            }
#endif
        }
    }
}