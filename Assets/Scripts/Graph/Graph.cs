using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pepe.graph
{
    public class Graph<T>
    {
        private Dictionary<T, GraphVertex<T>> vertices;

        public Graph()
        {
            vertices = new Dictionary<T, GraphVertex<T>>();
        }

        public void AddVertex(T value)
        {
            if (!vertices.ContainsKey(value))
            {
                vertices[value] = new GraphVertex<T>(value);
            }
        }

        public void AddEdge(T from, T to)
        {
            GetVertex(from).Neighbors.Add(GetVertex(to));
            GetVertex(to).Neighbors.Add(GetVertex(from));
        }

        public GraphVertex<T> GetVertex(T value)
        {
            if (!this.Contains(value))
                throw new ArgumentException(nameof(value), "The vertex is not in the graph");

            return vertices[value];
        }

        public bool Contains(T value)
        {
            return vertices.ContainsKey(value);
        }

        public IEnumerable<T> Vertices => vertices.Keys;

        public bool AreNeighbors(T vertex1, T vertex2)
        {
            return GetVertex(vertex1).IsNeighbor(vertex2);
        }
    }

}