using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using pepe.graph;
using pepe.pathfinding;


namespace pepe.pathfinding.tests
{
    public class PathfindingTests
    {
        [Test]
        public void PathfindingSimplestTest()
        {
            Vector3Graph graph = new Vector3Graph();
            graph.AddVertex(new Vector3(0, 0, 0));
            graph.AddVertex(new Vector3(0, 1, 0));
            graph.AddVertex(new Vector3(1, 0, 0));
            graph.AddVertex(new Vector3(1, 1, 0));
            graph.AddVertex(new Vector3(1, 2, 0));
            graph.AddVertex(new Vector3(2, 1, 0));
            graph.AddVertex(new Vector3(2, 2, 0));
            graph.AddVertex(new Vector3(3, 3, 0));

            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(1, 1, 0));

            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(0, 1, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(1, 0, 0));

            graph.AddEdge(new Vector3(1, 0, 0), new Vector3(0, 1, 0));

            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(2, 1, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(2, 2, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(1, 2, 0));

            graph.AddEdge(new Vector3(2, 2, 0), new Vector3(2, 1, 0));
            graph.AddEdge(new Vector3(2, 2, 0), new Vector3(1, 2, 0));

            graph.AddEdge(new Vector3(1, 2, 0), new Vector3(3, 3, 0));



            var res = Pathfinding.AStarShortestPath(graph, new Vector3(0, 0, 0), new Vector3(3, 3, 0));


        }

        [Test]
        public void PathfindingNonExistingVertex()
        {
            Vector3Graph graph = new Vector3Graph();
            graph.AddVertex(new Vector3(0, 0, 0));
            graph.AddVertex(new Vector3(0, 1, 0));
            graph.AddVertex(new Vector3(1, 0, 0));
            graph.AddVertex(new Vector3(1, 1, 0));
            graph.AddVertex(new Vector3(1, 2, 0));
            graph.AddVertex(new Vector3(2, 1, 0));
            graph.AddVertex(new Vector3(2, 2, 0));
            graph.AddVertex(new Vector3(3, 3, 0));

            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            graph.AddEdge(new Vector3(0, 0, 0), new Vector3(1, 1, 0));

            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(0, 1, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(1, 0, 0));

            graph.AddEdge(new Vector3(1, 0, 0), new Vector3(0, 1, 0));

            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(2, 1, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(2, 2, 0));
            graph.AddEdge(new Vector3(1, 1, 0), new Vector3(1, 2, 0));

            graph.AddEdge(new Vector3(2, 2, 0), new Vector3(2, 1, 0));
            graph.AddEdge(new Vector3(2, 2, 0), new Vector3(1, 2, 0));

            graph.AddEdge(new Vector3(1, 2, 0), new Vector3(3, 3, 0));





            Assert.Throws<ArgumentException>(() => Pathfinding.AStarShortestPath(graph, new Vector3(0, 0, 0), new Vector3(77, 77, 0)));
        }
        [Test]
        public void PathfindingNonConnectedGraph()
        {
            Vector3Graph graph = new Vector3Graph();
            graph.AddVertex(new Vector3(0, 0, 0));
            graph.AddVertex(new Vector3(0, 1, 0));
            graph.AddVertex(new Vector3(1, 0, 0));
            graph.AddVertex(new Vector3(1, 1, 0));
            graph.AddVertex(new Vector3(1, 2, 0));
            graph.AddVertex(new Vector3(2, 1, 0));
            graph.AddVertex(new Vector3(2, 2, 0));
            graph.AddVertex(new Vector3(3, 3, 0));


            Assert.Throws<ArgumentException>(() => Pathfinding.AStarShortestPath(graph, new Vector3(0, 0, 0), new Vector3(77, 77, 0)));
        }

        [Test]
        public void PathfindingBig()
        {
            int gridSize = 30;

            Vector3Graph graph = new Vector3Graph();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    graph.AddVertex(new Vector3(x, y, 0));

                }
            }

            foreach (var vertex in graph.Vertices)
            {
                if (vertex.x < gridSize - 1)
                { graph.AddEdge(vertex, vertex + Vector3.right); }

                if (vertex.y < gridSize - 1)
                { graph.AddEdge(vertex, vertex + Vector3.up); }


            }


            Vector3 vector1 = new Vector3(0, 0, 0);
            Vector3 vector2 = new Vector3(gridSize - 1, gridSize - 1, 0);



            Pathfinding.AStarShortestPath(graph, vector1, vector2);
        }


    }
}
