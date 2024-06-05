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

        [Test]
        [TestCase(3, 5)]
        [TestCase("hello", "world")]
        public void AddEdgeMultipleTimes<T>(T value1, T value2)
        {
            var graph = new Graph<T>();
            graph.AddVertex(value1);
            graph.AddVertex(value2);

            graph.AddEdge(value1, value2);
            graph.AddEdge(value1, value2);
            graph.AddEdge(value1, value2);

            Assert.IsTrue(graph.AreNeighbors(value1, value2), $"Vertex:{value1} and vertex:{value2} should be neighbors");
            Assert.IsTrue(graph.AreNeighbors(value2, value1), $"Vertex:{value2} and vertex:{value1} should be neighbors");
        }

        public static IEnumerable<object[]> GetGraphsAndConectedComponetsNumber()
        {

            var graph0 = new Graph<int>();
            graph0.AddVertex(0);
            graph0.AddVertex(1);
            graph0.AddVertex(2);

            var graph1 = new Graph<int>();
            graph1.AddVertex(0);
            graph1.AddVertex(1);
            graph1.AddVertex(2);
            graph1.AddEdge(0, 1);
            graph1.AddEdge(1, 2);

            var graph2 = new Graph<int>();
            graph2.AddVertex(1);
            graph2.AddVertex(2);
            graph2.AddVertex(3);
            graph2.AddVertex(4);
            graph2.AddVertex(5);
            graph2.AddVertex(6);
            graph2.AddVertex(7);
            graph2.AddVertex(8);
            graph2.AddVertex(9);
            graph2.AddVertex(10);
            graph2.AddVertex(11);
            graph2.AddVertex(12);
            graph2.AddEdge(7, 1);
            graph2.AddEdge(7, 2);
            graph2.AddEdge(7, 4);
            graph2.AddEdge(7, 8);
            graph2.AddEdge(7, 10);
            graph2.AddEdge(7, 9);
            graph2.AddEdge(2, 4);
            graph2.AddEdge(5, 11);
            graph2.AddEdge(3, 6);
            graph2.AddEdge(12, 6);

            var graph3 = new Graph<int>();
            graph3.AddVertex(1);
            graph3.AddVertex(2);
            graph3.AddVertex(3);
            graph3.AddVertex(4);
            graph3.AddVertex(5);
            graph3.AddVertex(6);
            graph3.AddVertex(7);
            graph3.AddVertex(8);
            graph3.AddVertex(9);
            graph3.AddVertex(10);
            graph3.AddVertex(11);
            graph3.AddVertex(12);
            graph3.AddEdge(7, 1);
            graph3.AddEdge(7, 2);
            graph3.AddEdge(7, 4);
            graph3.AddEdge(7, 8);
            graph3.AddEdge(7, 10);
            graph3.AddEdge(7, 9);
            graph3.AddEdge(2, 4);
            graph3.AddEdge(5, 11);
            graph3.AddEdge(3, 6);
            graph3.AddEdge(12, 6);
            graph3.AddEdge(12, 11);

            var graph4 = new Graph<int>();
            graph4.AddVertex(1);
            graph4.AddVertex(2);
            graph4.AddVertex(3);
            graph4.AddVertex(4);
            graph4.AddVertex(5);
            graph4.AddVertex(6);
            graph4.AddVertex(7);
            graph4.AddVertex(8);
            graph4.AddVertex(9);
            graph4.AddVertex(10);
            graph4.AddVertex(11);
            graph4.AddVertex(12);
            graph4.AddVertex(13);
            graph4.AddEdge(7, 1);
            graph4.AddEdge(7, 2);
            graph4.AddEdge(7, 4);
            graph4.AddEdge(7, 8);
            graph4.AddEdge(7, 10);
            graph4.AddEdge(7, 9);
            graph4.AddEdge(2, 4);
            graph4.AddEdge(5, 11);
            graph4.AddEdge(3, 6);
            graph4.AddEdge(12, 6);
            graph4.AddEdge(12, 11);
            graph4.AddEdge(10, 11);


            yield return new object[] { graph0, 3 };
            yield return new object[] { graph1, 1 };
            yield return new object[] { graph2, 3 };
            yield return new object[] { graph3, 2 };
            yield return new object[] { graph4, 2 };
        }

        [Test, TestCaseSource(nameof(GetGraphsAndConectedComponetsNumber))]
        public void ConectedComponents<T>(Graph<T> graph, int count)
        {
            Assert.AreEqual(graph.ConectectedComponentsNumber(), count, $"This graph should have {count} conected components");
            Assert.AreEqual(graph.IsConected(), count <= 1, $"This graph should {(count <= 1 ? "" : "not")} be conected");
        }



        [Test]
        public void ConectedComponentsAre0()
        {
            var graph = new Graph<int>();
            Assert.AreEqual(graph.ConectectedComponentsNumber(), 0, "A graph without vertices should have 0 connected componets");
        }





    }
}