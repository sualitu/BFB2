namespace BattleForBetelgeuse {
    using BattleForBetelgeuse.GameElements.Units;
    using BattleForBetelgeuse.GUI.Hex;

    using UnityEngine;

    public class GridManager {
        private const int MapWidth = 30;

        private const int MapHeight = 30;

        private static GridManager instance;

        private readonly GameObject hexPrefab;
         
        private float hexHeight;

        private float hexWidth;

        private TileType[][] map;

        private GridManager() {
            hexPrefab = BehaviourUpdater.Prefabs.HexPrefab;
            CalculateDiameter();
            BuildMap();
            BuildGrid();
        }

        public static GridManager Instance {
            get {
                if (instance == null) {
                    instance = new GridManager();
                }
                return instance;
            }
        }

        private void BuildGrid() {
            for (var i = 0; i < map.Length; i++) {
                for (var j = 0; j < map[i].Length; j++) {
                    if (map[i][j] == TileType.NORMAL) {
                        InstantiateHexAt(i, j);
                    }
                }
            }
        }

        private void BuildMap() {
            map = new TileType[MapHeight][];
            for (var i = 0; i < MapHeight; i++) {
                map[i] = new TileType[MapWidth];
                for (var j = 0; j < MapWidth; j++) {
                    var x = Random.Range(0, 100);
                    if (x > 0) {
                        map[i][j] = TileType.NORMAL;
                    } else {
                        map[i][j] = TileType.NONE;
                    }
                }
            }
        }

        public bool MoveableHex(HexCoordinate hex) {
            try {
                var type = map[hex.X][hex.Y];
                return type == TileType.NORMAL && !UnitStore.Instance.IsUnitAtTile(hex);
            } catch {
                return false;
            }
        }

        private void InstantiateHexAt(int x, int y) {
            var hex = (GameObject)Object.Instantiate(hexPrefab, CalculateLocationFromXY(x, y), Quaternion.identity);
            var hexBehaviour = hex.gameObject.GetComponent<HexTileBehaviour>();
            hexBehaviour.Coordinate = new HexCoordinate(x, y);
        }

        private void CalculateDiameter() {
            var mesh = hexPrefab.GetComponent<MeshFilter>().sharedMesh;
            hexWidth = mesh.bounds.size.x;
            hexHeight = mesh.bounds.size.z;
        }

        public static GridManager Init() {
            return Instance;
        }

        private Vector3 CalculateLocationFromXY(int x, int y) {
            var xPos = x * hexWidth - (y % 2 == 0 ? 0 : hexWidth / 2);
            var yPos = y * hexHeight * .75f;
            return new Vector3(xPos, 0, yPos);
        }

        public static Vector3 CalculateLocationFromHexCoordinate(HexCoordinate coordinate) {
            return Instance.CalculateLocationFromXY(coordinate.X, coordinate.Y);
        }
    }

    internal enum TileType {
        NORMAL,

        NONE
    }
}