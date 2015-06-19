using System.Collections.Generic;
using System;
using UnityEngine;

namespace BattleForBetelgeuse.PathFinding {

  public class AStar<T> where T : IPathable { 

    public static List<T> FindPath(T start, T goal) {
      // Setup
      HashSet<T> closedSet = new HashSet<T>();

      HashSet<T> openSet = new HashSet<T>();
      openSet.Add(start);

      Dictionary<T, T> cameFrom = new Dictionary<T, T>(); 

      Dictionary<T, int> goalScore = new Dictionary<T, int>();
      goalScore[start] = 0;

      Dictionary<T, int> estimatedScore = new Dictionary<T, int>();
      estimatedScore[start] = goalScore[start] + start.EstimateCostTo(goal);

      // Find path
      while(openSet.Count > 0) {
        var current = LowestScoringNode(openSet, estimatedScore);
        if(current.Equals(goal)) {
          return ReconstructPath(cameFrom, current);
        }
        openSet.Remove(current);
        closedSet.Add(current);
        if(!current.IsMoveable()) {
          continue;
        }
        foreach(var neighbor in current.Neighbors<T>()) {
          if(closedSet.Contains(neighbor)) {
            continue;
          }
          var tentativeScore = goalScore[current] + 1;

          if(!openSet.Contains(neighbor) || tentativeScore < goalScore[neighbor]) {
            cameFrom[neighbor] = current;
            goalScore[neighbor] = tentativeScore;
            estimatedScore[neighbor] = goalScore[neighbor] + neighbor.EstimateCostTo(goal);
            openSet.Add(neighbor);
          }
        }
      }

      // No path was found
      throw new PathNotFoundException(String.Format("Could not find path between {0} and {1}", start, goal));
    }

    static T LowestScoringNode(HashSet<T> openSet, Dictionary<T, int> estimatedScore) {
      var currentBest = int.MaxValue;
      T current = default(T);
      foreach(var p in openSet) {
        var estValue = estimatedScore[p];
        if(estValue < currentBest) {
          current = p;
          currentBest = estValue;
        }      
      }
      return current;
    }

    static List<T> ReconstructPath(Dictionary<T, T> cameFrom, T current) {
      List<T> totalPath = new List<T>() { current };
      while(cameFrom.ContainsKey(current)) {
        current = cameFrom[current];
        totalPath.Add(current);
      }
      return totalPath;
    }
  }
}

