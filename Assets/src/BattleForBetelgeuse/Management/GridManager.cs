namespace Assets.BattleForBetelgeuse.Management {
    using System.IO;

    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;

    using UnityEngine;

    public class GridManager {
        private static GridManager instance;

        private readonly GameObject basePrefab;

        private readonly GameObject hexPrefab;

        private float hexHeight;

        private float hexWidth;

        private TileType[][] map;

        private GridManager() {
            hexPrefab = Main.PrefabManager.HexPrefab;
            basePrefab = Main.PrefabManager.BasePrefab;
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
                    switch (map[i][j]) {
                        case TileType.Base:
                            InstantiateBaseAt(j, i);
                            break;
                        case TileType.Normal:
                            InstantiateHexAt(j, i);
                            break;
                    }
                }
            }
        }

        private void InstantiateBaseAt(int x, int y) {
            Object.Instantiate(basePrefab, CalculateLocationFromXY(x, y), Quaternion.identity);
        }

        private void BuildMap() {
            try {
                var json = JSONObject.Create(File.ReadAllText(@"maps/StandardMap.json"));

                var heightJson = json["height"].ToString();
                var widthJson = json["width"].ToString();
                var height = int.Parse(heightJson);
                var width = int.Parse(widthJson);

                var data = json["layers"][0]["data"];

                map = new TileType[height][];

                for (int i = 0, k = 0; i < height; i++) {
                    map[i] = new TileType[width];
                    for (var j = 0; j < width; j++) {
                        var x = int.Parse(data[k].ToString());
                        switch (x) {
                            case 14:
                                map[i][j] = TileType.None;
                                break;
                            case 7:
                                map[i][j] = TileType.Base;
                                break;
                            default:
                                map[i][j] = TileType.Normal;
                                break;
                        }
                        k++;
                    }
                }
            } catch {
                Debug.Log("Parsing of map failed");
            }
        }

        public bool MoveableHex(HexCoordinate hex) {
            try {
                var type = map[hex.Y][hex.X];
                return type != TileType.None && !UnitStore.Instance.IsUnitAtTile(hex);
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
        Normal,

        None, // 14

        Base, // 7

        Flag,

        Event,

        NpcCamp
    }
}