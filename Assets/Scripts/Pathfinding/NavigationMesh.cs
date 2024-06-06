using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pepe.graph;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace pepe.pathfinding
{
    public class NavigationMesh : MonoBehaviour
    {


        Vector3Graph graph = new Vector3Graph();

        [SerializeField] float maxSize = 13.5f;
        [SerializeField] float step = 1;

        [SerializeField] LayerMask obstaclesLayer;
        [SerializeField] NavigationMeshGizmosHelper gizmosHelper;

        void Start()
        {
            SpawnMesh();
        }

        private void SpawnMesh()
        {

            foreach (var position in vertexPositionsGrid(new Vector3(1.5f, 0, 1.5f), maxSize, step))
            {

                addVertexToGraph(position);

            }

            foreach (var vertex in graph.Vertices)
            {
                if (vertex.x < maxSize)
                { addEdgeToGraph(vertex, vertex + Vector3.right * step); }

                if (vertex.z < maxSize)
                { addEdgeToGraph(vertex, vertex + Vector3.forward * step); }

                if (vertex.x < maxSize && vertex.z < maxSize)
                { addEdgeToGraph(vertex, vertex + (Vector3.forward + Vector3.right) * step); }
                { addEdgeToGraph(vertex + Vector3.right * step, vertex + Vector3.forward * step); }
            }
        }

        List<Vector3> vertexPositionsGrid(Vector3 startingPosition, float maxPosition, float step)
        {
            List<Vector3> vertexPositions = new List<Vector3>();

            for (float x = startingPosition.x; x <= maxPosition; x += step)
            {
                for (float y = startingPosition.z; y <= maxPosition; y += step)
                {
                    vertexPositions.Add(new Vector3(x, 0, y));
                }
            }

            return vertexPositions;
        }

        public void addVertexToGraph(Vector3 vertex)
        {
            if (!Physics.CheckSphere(vertex, 0.3f, obstaclesLayer))
            {
                graph.AddVertex(vertex);
                gizmosHelper.AddVertex(vertex);
            }

        }

        public void addEdgeToGraph(Vector3 vertexSource, Vector3 vertexSink)
        {
            if (!graph.Contains(vertexSource) || !graph.Contains(vertexSink))
            {
                return;
            }

            if (!Physics.Raycast(vertexSource, vertexSink - vertexSource, Vector3.Distance(vertexSource, vertexSink), obstaclesLayer))
            {

                gizmosHelper.AddLine(vertexSource, vertexSink);
                graph.AddEdge(vertexSource, vertexSink);
            }
        }

        public List<Vector3> GetPath(Vector3 source, Vector3 sink)
        {
            List<Vector3> path = Pathfinding.AStarShortestPath(graph, source, sink);
            gizmosHelper.UpdatePath(path);
            return path;
        }

        public async Task<List<Vector3>> GetPathAsync(Vector3 source, Vector3 sink)
        {
            List<Vector3> path = await Task.Run(() => Pathfinding.AStarShortestPath(graph, source, sink));
            gizmosHelper.UpdatePath(path);
            return path;
        }

        public Vector3 GetClosestVertexToPosition(Vector3 position)
        {
            List<Vector3> vertexByDistance = graph.Vertices.OrderBy(x => Vector3.Distance(x, position)).ToList();

            foreach (var vertex in vertexByDistance)
            {
                if (!Physics.Raycast(vertex, position - vertex, Vector3.Distance(vertex, position), obstaclesLayer))
                {
                    return vertex;
                }
            }

            throw new Exception("There are 0 vertex available");
        }


    }
}