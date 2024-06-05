using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pepe.graph;

public class NavigatorMeshSpawner : MonoBehaviour
{

    Dictionary<Vector3, GameObject> spawns = new Dictionary<Vector3, GameObject>();
    Vector3Graph graph = new Vector3Graph();

    [SerializeField] float maxSize = 13.5f;
    [SerializeField] float step = 1;

    [SerializeField] LayerMask obstaclesLayer;

    void Start()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * 0.3f;






        foreach (var position in vertexPositionsGrid(new Vector3(1.5f, 0, 1.5f), maxSize, step))
        {




            addVertexToGraph(position);

        }

        foreach (var vertex in graph.Vertices)
        {

            { addEdgeToGraph(vertex, vertex + Vector3.right * step); }


            { addEdgeToGraph(vertex, vertex + Vector3.forward * step); }


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
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.3f;
            var go = Instantiate(sphere, vertex, Quaternion.identity);
            spawns[vertex] = go;
        }

    }

    public struct Line
    {
        public Vector3 start;
        public Vector3 end;
        public Color color;

        public Line(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
            this.color = Color.red;
        }
    }

    // Lista pública de líneas para dibujar
    public List<Line> lines = new List<Line>();

    // Método para dibujar las líneas usando Gizmos
    private void OnDrawGizmos()
    {
        if (lines == null)
            return;

        foreach (var line in lines)
        {
            Gizmos.color = line.color;
            Gizmos.DrawLine(line.start, line.end);
        }
    }
    public void addEdgeToGraph(Vector3 vertexSource, Vector3 vertexSink)
    {
        if (!graph.Contains(vertexSource) || !graph.Contains(vertexSink))
        {
            return;
        }

        RaycastHit hit;
        if (!Physics.Raycast(vertexSource, vertexSink - vertexSource, out hit, Vector3.Distance(vertexSource, vertexSink), obstaclesLayer))
        {


            lines.Add(new Line(vertexSource, vertexSink));
            graph.AddEdge(vertexSource, vertexSink);
        }
    }


}
