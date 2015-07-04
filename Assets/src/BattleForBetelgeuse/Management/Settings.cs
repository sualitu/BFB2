namespace Assets.GameManagement {
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

            public static readonly Vector3 LevelAreaMax = new Vector3(23, 10, 12);

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
        }
    }
}