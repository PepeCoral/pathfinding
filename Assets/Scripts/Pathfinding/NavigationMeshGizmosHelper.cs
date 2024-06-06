using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace pepe.pathfinding
{
    public class NavigationMeshGizmosHelper : MonoBehaviour
    {
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
                line.SetColor(Color.yellow);
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                var source = path[i];
                var sink = path[i + 1];

                lines[new HashSet<Vector3>() { source, sink }].SetColor(Color.red);
            }
        }



        private void OnDrawGizmos()
        {
            if (lines == null)
                return;

            foreach (var line in lines.Values)
            {
                Gizmos.color = line.color;
                Gizmos.DrawLine(line.start, line.end);


            }

            foreach (var vertex in vertices)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(vertex, 0.05f);
            }
        }
    }
}