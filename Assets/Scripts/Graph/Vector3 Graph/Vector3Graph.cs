using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace pepe.graph
{
    public class Vector3Graph : Graph<Vector3>
    {
        private Dictionary<HashSet<Vector3>, float> distanceMemory = new Dictionary<HashSet<Vector3>, float>(HashSet<Vector3>.CreateSetComparer());

        public float Distance(GraphVertex<Vector3> vertex1, GraphVertex<Vector3> vertex2)
        {
            if (vertex1 == null || vertex2 == null)
                throw new ArgumentNullException();

            return Distance(vertex1, vertex2);
        }

        public float Distance(Vector3 vector1, Vector3 vector2)
        {
            if (vector1 == null || vector2 == null)
                throw new ArgumentNullException();

            HashSet<Vector3> vectorSet = new HashSet<Vector3>() { vector1, vector2 };

            Debug.Assert(vectorSet.Count == 2);

            if (isDistanceInMemory(vectorSet))
                return distanceMemory[vectorSet];

            float distance = Vector3.Distance(vector1, vector2);
            distanceMemory.Add(vectorSet, distance);
            return distance;
        }

        private bool isDistanceInMemory(Vector3 vector1, Vector3 vector2)
        {
            return isDistanceInMemory(new HashSet<Vector3>() { vector1, vector2 });
        }
        private bool isDistanceInMemory(HashSet<Vector3> vectorSet)
        {
            return distanceMemory.ContainsKey(vectorSet);
        }
    }
}