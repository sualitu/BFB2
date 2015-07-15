namespace Assets.BattleForBetelgeuse.Management {
    using UnityEngine;

    public class Settings {
        public const string Version = "a1";

        public class Camera {
            public const int ScrollSpeed = 15;

            public const float KeyboardScrollSpeed = .5f;

            public const int ScrollArea = 25;

            public const int ZoomSpeed = 25;

            public const int PanSpeed = 100;

            public const int PanAngleMin = 20;

            public const int PanAngleMax = 50;

            public const int DragSpeed = 100;

            public static readonly Vector3 LevelAreaMax = new Vector3(12, 10, 9);

            public static readonly Vector3 LevelAreaMin = new Vector3(0, 1, -3);

            public static readonly float ZoomMax = LevelAreaMax.y;
        }

        public class ColorSettings {
            public static Color TileBaseColor = new Color(0.72f, 0.72f, 0.72f, 0.8f);
        }

        public class Animations {
            public static bool AnimateMovement = true;

            public static bool AnimateDeath = true;

            public static bool AnimateCombat = true;

            public class Cards {
                public const float FadeInTime = 1.2f;

                public const float CardMovementSlow = .75f;

                public const float CardMovementDuration = .5f;

                public const float CardSize = .7f;

                public const float CardMovementFast = .2f;

                public static Vector3 CardSpawnPosition = new Vector3((Screen.width / 2) *.6f, 0f, 0f);

                public static Vector3 CardPlayedPosition = new Vector3(-(Screen.width / 2) * .6f, 0f, 0f);

                public static Vector3 CardDropZoneCutOff = new Vector3(0f, Screen.height /4, 0f);
            }
        }

        public class GameSettings {
            public static int MaxMana = 20;

            public static int MaxHandSize = 7;
        }
    }
}