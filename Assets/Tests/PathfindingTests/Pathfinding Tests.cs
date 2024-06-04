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
            res.Reverse();
            foreach (var i in res)
            { Debug.Log(i); }

        }

    }
}
