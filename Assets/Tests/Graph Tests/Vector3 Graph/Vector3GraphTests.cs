using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;



namespace pepe.graph.tests
{
    public class Vector3GraphTest
    {
        public static IEnumerable<Vector3[]> GetTestsVectors()
        {
            yield return new Vector3[] { Vector3.down, Vector3.up };
            yield return new Vector3[] { Vector3.up, Vector3.down };
            yield return new Vector3[] { Vector3.left, Vector3.up };
            yield return new Vector3[] { Vector3.zero, Vector3.up };
            yield return new Vector3[] { Vector3.zero, Vector3.zero };
            yield return new Vector3[] { Vector3.down, Vector3.forward };
            yield return new Vector3[] { UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100), UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100) };
            yield return new Vector3[] { UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100), UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100) };
            yield return new Vector3[] { UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100), UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100) };
            yield return new Vector3[] { UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100), UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-100, 100) };
        }

        [Test, TestCaseSource(nameof(GetTestsVectors))]
        public void CalculateDistance(Vector3 vector1, Vector3 vector2)
        {
            Vector3Graph graph = new Vector3Graph();
            graph.AddVertex(vector1);
            graph.AddVertex(vector2);

            float distance = Vector3.Distance(vector1, vector2);
            Assert.AreEqual(distance, graph.Distance(vector1, vector2), $"Distance should be {distance}");
        }



    }
}