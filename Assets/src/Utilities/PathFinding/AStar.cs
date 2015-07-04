namespace Assets.Utilities.PathFinding {
    using System.Collections.Generic;

    public class AStar<T>
        where T : IPathable {
        public static List<T> FindPath(T start, T goal) {
            // Setup
            var closedSet = new HashSet<T>();

            var openSet = new HashSet<T>();
            openSet.Add(start);

            var cameFrom = new Dictionary<T, T>();

            var goalScore = new Dictionary<T, int>();
            goalScore[start] = 0;

            var estimatedScore = new Dictionary<T, int>();
            estimatedScore[start] = goalScore[start] + start.EstimateCostTo(goal);

            // Find path
            while (openSet.Count > 0) {
                var current = LowestScoringNode(openSet, estimatedScore);
                if (current.Equals(goal)) {
                    return ReconstructPath(cameFrom, current);
                }
                openSet.Remove(current);
                closedSet.Add(current);
                foreach (var neighbor in current.Neighbors<T>()) {
                    if (!neighbor.Equals(goal) && !neighbor.IsMoveable()) {
                        closedSet.Add(neighbor);
                    }

                    if (closedSet.Contains(neighbor)) {
                        continue;
                    }
                    var tentativeScore = goalScore[current] + 1;

                    if (!openSet.Contains(neighbor) || tentativeScore < goalScore[neighbor]) {
                        cameFrom[neighbor] = current;
                        goalScore[neighbor] = tentativeScore;
                        estimatedScore[neighbor] = goalScore[neighbor] + neighbor.EstimateCostTo(goal);
                        openSet.Add(neighbor);
                    }
                }
            }

            // No path was found
            throw new PathNotFoundException(string.Format("Could not find path between {0} and {1}", start, goal));
        }

        private static T LowestScoringNode(HashSet<T> openSet, Dictionary<T, int> estimatedScore) {
            var currentBest = int.MaxValue;
            var current = default(T);
            foreach (var p in openSet) {
                var estValue = estimatedScore[p];
                if (estValue < currentBest) {
                    current = p;
                    currentBest = estValue;
                }
            }
            return current;
        }

        private static List<T> ReconstructPath(Dictionary<T, T> cameFrom, T current) {
            var totalPath = new List<T> { current };
            while (cameFrom.ContainsKey(current)) {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            return totalPath;
        }
    }
}