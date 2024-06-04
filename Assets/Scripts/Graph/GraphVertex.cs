using System.Collections.Generic;
using System;


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
    }
}