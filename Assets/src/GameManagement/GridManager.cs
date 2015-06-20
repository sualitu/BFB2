using UnityEngine;
using BattleForBetelgeuse;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.GameElements.Unit;

namespace BattleForBetelgeuse {

  public class GridManager {
    private static GridManager instance;
    float hexWidth;
    float hexHeight;
    const int mapWidth = 10;
    const int mapHeight = 10;
    TileType[][] map;
        
    public static GridManager Instance { 
      get {
        if(instance == null) {
          instance = new GridManager();
        }
        return instance;
      } 
    }

    private GameObject hexPrefab;

    private GridManager() {
      hexPrefab = BehaviourUpdater.Prefabs.HexPrefab;
      CalculateDiameter();
      BuildMap();
      BuildGrid();
    }

    private void BuildGrid() {
      for(var i = 0; i < map.Length; i++) {
        for(int j = 0; j < map[i].Length; j++) {
          if(map[i][j] == TileType.NORMAL) {
            InstantiateHexAt(i, j);
          }
        }
      }
    }

    void BuildMap() {
      map = new TileType[mapHeight][];
      for(var i = 0; i < mapHeight; i++) {
        map[i] = new TileType[mapWidth];
        for(int j = 0; j < mapWidth; j++) {
          var x = Random.Range(0, 100);
          if(x > 5) {
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
      var hex = (GameObject)GameObject.Instantiate(hexPrefab, CalculateLocationFromXY(x, y), Quaternion.identity);
      var hexBehaviour = hex.gameObject.GetComponent<HexTileBehaviour>();
      hexBehaviour.Coordinate = new HexCoordinate(x, y);
    }

    void CalculateDiameter() {
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

  enum TileType {
    NORMAL,
    NONE
  }
}

