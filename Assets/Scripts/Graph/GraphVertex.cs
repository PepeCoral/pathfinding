using System.Collections.Generic;
using System;
using System.Linq;


namespace pepe.graph
{
    public class GraphVertex<T>
    {

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value ?? throw new ArgumentNullException(nameof(value), "Vertex value cannot be null");
            }
        }
        public List<GraphVertex<T>> Neighbors { get; }

        public GraphVertex(T value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value), "Vertex value cannot be null");
            Neighbors = new List<GraphVertex<T>>();
        }


        public bool IsNeighbor(GraphVertex<T> vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException(nameof(vertex), "vertex cannot be null");

            return IsNeighbor(vertex.Value);
        }
        public bool IsNeighbor(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "vertex cannot be null");
            return Neighbors.Select(e => e.Value).Any(e => e.Equals(value));
        }
    }
}