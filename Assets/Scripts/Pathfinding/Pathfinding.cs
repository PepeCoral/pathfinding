using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pepe.graph;
using System;
using System.Linq;


namespace pepe.pathfinding
{
    public static class Pathfinding
    {
        public static List<Vector3> AStarShortestPath(Vector3Graph graph, Vector3 source, Vector3 target)
        {
            if (!graph.Contains(source) || !graph.Contains(target))
                throw new ArgumentException("Graph must contain source and target vertices");

            if (!graph.IsConected())
                throw new ArgumentException("Graph must be conected");

            AStarData vertexToExplore = new AStarData(source, null, 0, graph.Distance(source, target));
            vertexToExplore.isVisited = true;
            Graph<AStarData> exploringGraph = new Graph<AStarData>();

            exploringGraph.AddVertex(vertexToExplore);

            while (vertexToExplore.vector != target)
            {
                //Explore Vertices
                foreach (var vector3Vertex in graph.GetVertex(vertexToExplore.vector).Neighbors)
                {

                    if (!exploringGraph.Vertices.Any(x => x.vector == vector3Vertex.Value))
                    {
                        float distance = graph.Distance(vector3Vertex.Value, vertexToExplore.vector);
                        AStarData vertexExploring = new AStarData(vector3Vertex.Value,
                            vertexToExplore,
                            vertexToExplore.distanceToSource + distance,
                            graph.Distance(vector3Vertex.Value, vertexToExplore.vector));

                        exploringGraph.AddVertex(vertexExploring);
                        exploringGraph.AddEdge(vertexExploring, vertexToExplore);
                    }
                }


                vertexToExplore = exploringGraph.Vertices.Where(x => !x.isVisited).Min();
                exploringGraph.Vertices.Where(x => !x.isVisited).Min().isVisited = true;

                if (exploringGraph.Vertices.Any(x => x.vector == target))
                { vertexToExplore = exploringGraph.Vertices.Where(x => x.vector == target).First(); }
            }

            List<Vector3> path = new List<Vector3>();
            AStarData backtrackingVertex = vertexToExplore;
            while (backtrackingVertex.commingVector != null)
            {
                path.Add(backtrackingVertex.vector);
                backtrackingVertex = backtrackingVertex.commingVector;
            }

            path.Add(backtrackingVertex.vector);
            path.Reverse();
            return path;
        }

    }

    public class AStarData : IComparable<AStarData>
    {
        public Vector3 vector;
        public AStarData commingVector;
        public float distanceToSource, distanceToTarget;
        public float totalDistance => distanceToSource + distanceToTarget;
        public bool isVisited = false;

        public AStarData(Vector3 vector, AStarData commingVector, float distanceToSource, float distanceToTarget)
        {
            this.vector = vector;
            this.commingVector = commingVector;
            this.distanceToSource = distanceToSource;
            this.distanceToTarget = distanceToTarget;
        }
        public int CompareTo(AStarData obj)
        {
            if (obj == null) return 1;

            int totalDistanceComparison = this.totalDistance.CompareTo(obj.totalDistance);

            return totalDistanceComparison != 0 ? totalDistanceComparison : this.distanceToTarget.CompareTo(obj.distanceToTarget);
        }

    }
}
