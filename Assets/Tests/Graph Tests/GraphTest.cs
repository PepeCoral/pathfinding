using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;



namespace pepe.graph.tests
{
    public class GraphTest
    {


        public static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(1).SetName("Int");
            yield return new TestCaseData("test").SetName("String");
            yield return new TestCaseData(3.14).SetName("Float");
            yield return new TestCaseData(new Vector3(1, 2, 3)).SetName("Vector3");
        }

        [Test, TestCaseSource(nameof(GetTestCases))]
        public void AddNewVertex<T>(T value)
        {
            var graph = new Graph<T>();
            graph.AddVertex(value);
            Assert.AreEqual(value, graph.GetVertex(value).Value, $"Vertex Value Should be {value}");
        }


        [Test]
        public void AddNullVertex()
        {
            var graph = new Graph<List<int>>();
            Assert.Throws<ArgumentNullException>(() => graph.AddVertex(null), "The graph cannot add a vertex that is null");
        }

        [Test, TestCaseSource(nameof(GetTestCases))]
        public void ContainsVertex<T>(T value)
        {
            var graph = new Graph<T>();
            graph.AddVertex(value);

            Assert.IsTrue(graph.Contains(value), $"The graph should contain {value}");
        }

        [Test]
        [TestCase(4, 5)]
        [TestCase(4f, 5.5f)]
        [TestCase("hello", "bye")]
        public void DoesNotContainVertex<T>(T contained, T notContained)
        {

            var graph = new Graph<T>();
            graph.AddVertex(contained);
            Assert.IsFalse(graph.Contains(notContained), $"The graph should not contain{notContained}");
        }

        public void GetNonExistingVertex<T>(T contained, T notContained)
        {
            var graph = new Graph<T>();
            graph.AddVertex(contained);
            Assert.Throws<ArgumentException>(() => graph.GetVertex(notContained), "The graph cannot return a vertex that is not stored");
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AddEdge<T>(T from, T to)
        {
            var graph = new Graph<T>();
            graph.AddVertex(from);
            graph.AddVertex(to);
            graph.AddEdge(from, to);

            Assert.IsTrue(graph.GetVertex(from).IsNeighbor(to), "Origin vertex should be neighbor of destiny vertex");
            Assert.IsTrue(graph.GetVertex(to).IsNeighbor(from), "Destiny vertex should be neighbor of origin vertex");

        }

        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AddEdgeToNonExistingVertexDestiny<T>(T from, T to)
        {
            var graph = new Graph<T>();
            graph.AddVertex(from);

            Assert.Throws<ArgumentException>(() => graph.AddEdge(from, to), "Destiny vertex should be in the graph");

        }

        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AddEdgeToNonExistingVertexOrigin<T>(T from, T to)
        {
            var graph = new Graph<T>();
            graph.AddVertex(to);

            Assert.Throws<ArgumentException>(() => graph.AddEdge(from, to), "Origin vertex should be in the graph");

        }

        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AreNeighbor<T>(T vertex1, T vertex2)
        {
            var graph = new Graph<T>();
            graph.AddVertex(vertex1);
            graph.AddVertex(vertex2);
            graph.AddEdge(vertex1, vertex2);

            Assert.IsTrue(graph.AreNeighbors(vertex1, vertex2), $"Vertex:{vertex1} and vertex:{vertex2} should be neighbors");
            Assert.IsTrue(graph.AreNeighbors(vertex2, vertex1), $"Vertex:{vertex2} and vertex:{vertex1} should be neighbors");

        }


        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AreNotNeighbor<T>(T vertex1, T vertex2)
        {
            var graph = new Graph<T>();
            graph.AddVertex(vertex1);
            graph.AddVertex(vertex2);

            Assert.IsFalse(graph.AreNeighbors(vertex1, vertex2), $"Vertex:{vertex1} and vertex:{vertex2} should be neighbors");
            Assert.IsFalse(graph.AreNeighbors(vertex2, vertex1), $"Vertex:{vertex2} and vertex:{vertex1} should be neighbors");

        }

        [Test]
        public void AreNeighborsNull()
        {
            var graph = new Graph<List<int>>();

            var vertex1 = new List<int>() { 1 };
            var vertex2 = new List<int>() { 2 };
            graph.AddVertex(vertex1);
            graph.AddVertex(vertex2);

            Assert.Throws<ArgumentNullException>(() => graph.AreNeighbors(null, vertex2), "First argument cannot be null");
            Assert.Throws<ArgumentNullException>(() => graph.AreNeighbors(vertex1, null), "Second argument cannot be null");

        }



    }
}