using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace pepe.graph
{
    public class GraphVertex<T>
    {
        public T Value { get; set; }
        public List<GraphVertex<T>> Neighbors { get; set; }

        public GraphVertex(T value)
        {
            Value = value;
            Neighbors = new List<GraphVertex<T>>();
        }
    }
}