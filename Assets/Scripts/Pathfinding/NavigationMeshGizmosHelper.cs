using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pepe.pathfinding
{
    public class NavigationMeshGizmosHelper : MonoBehaviour
    {
        //Gizmos Options
        private enum GizmosDisplay { OFF = 0, ON = 1 };
        private enum VertexDisplay { OFF = 0, ON = 1 };
        private enum EdgesDisplay { OFF = 0, GRID = 1, GRID_AND_PATH = 2, PATH = 3 };


        GizmosDisplay gizmosDisplay = GizmosDisplay.OFF;
        VertexDisplay vertexDisplay = VertexDisplay.ON;
        EdgesDisplay edgesDisplay = EdgesDisplay.GRID_AND_PATH;


        Color edgeNormalColor = Color.yellow;
        Color edgePathColor = Color.red;
        Color vertexColor = Color.magenta;

        public class Line
        {
            public Vector3 start;
            public Vector3 end;
            public Color color;

            public Line(Vector3 start, Vector3 end)
            {
                this.start = start;
                this.end = end;
                this.color = Color.yellow;
            }

            public void SetColor(Color color)
            { this.color = color; }
        }

        Dictionary<HashSet<Vector3>, Line> lines = new Dictionary<HashSet<Vector3>, Line>(HashSet<Vector3>.CreateSetComparer());
        List<Vector3> vertices = new List<Vector3>();
        public void AddLine(Vector3 source, Vector3 sink)
        {
            lines[new HashSet<Vector3>() { source, sink }] = new Line(source, sink);
        }

        public void AddVertex(Vector3 vertex)
        { vertices.Add(vertex); }

        public void UpdatePath(List<Vector3> path)
        {
            foreach (var line in lines.Values)
            {
                line.SetColor(edgeNormalColor);
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                var source = path[i];
                var sink = path[i + 1];

                lines[new HashSet<Vector3>() { source, sink }].SetColor(edgePathColor);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                gizmosDisplay++;
                gizmosDisplay = (GizmosDisplay)(((int)gizmosDisplay) % Enum.GetNames(typeof(GizmosDisplay)).Length);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                vertexDisplay++;
                vertexDisplay = (VertexDisplay)(((int)vertexDisplay) % Enum.GetNames(typeof(VertexDisplay)).Length);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                edgesDisplay++;
                edgesDisplay = (EdgesDisplay)(((int)edgesDisplay) % Enum.GetNames(typeof(EdgesDisplay)).Length);
            }
        }



        private void OnDrawGizmos()
        {
            if (gizmosDisplay == GizmosDisplay.OFF)
                return;

            if (edgesDisplay != EdgesDisplay.OFF)
            {
                foreach (var line in lines.Values)
                {
                    if (edgesDisplay != EdgesDisplay.PATH || line.color == edgePathColor)
                    {
                        Gizmos.color = line.color;

                        if (edgesDisplay == EdgesDisplay.GRID)
                        { Gizmos.color = edgeNormalColor; }

                        Gizmos.DrawLine(line.start, line.end);
                    }

                }
            }

            if (vertexDisplay != VertexDisplay.OFF)
            {
                foreach (var vertex in vertices)
                {
                    Gizmos.color = vertexColor;
                    Gizmos.DrawSphere(vertex, 0.05f);
                }
            }
        }
    }
}