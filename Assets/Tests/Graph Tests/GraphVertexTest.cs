using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using pepe.graph;

public class GraphVertexTest
{

    [Test]
    public void CreateNewVertex()
    {
        GraphVertex<int> vertex = new GraphVertex<int>(5);
        Assert.AreEqual(5, vertex.Value, "Vertex Value Should be 5");
    }

    [Test]
    public void CreateNewVertexWithString()
    {
        GraphVertex<string> vertex = new GraphVertex<string>("hello world!");
        Assert.AreEqual("hello world!", vertex.Value, "Vertex value should be hello world!");
    }

    [Test]
    public void CreateNewVertexVector3()
    {
        GraphVertex<Vector3> vertex = new GraphVertex<Vector3>(Vector3.up);
        Assert.AreEqual(Vector3.up, vertex.Value, "Vertex value should be a vector (0,1,0)");
    }

    [Test]
    public void CreateNewNullVertex()
    {
        Assert.Throws<ArgumentNullException>(() => new GraphVertex<List<int>>(null));
    }

    [Test]
    public void SetNullValue()
    {
        GraphVertex<List<int>> vertex = new GraphVertex<List<int>>(new List<int> { 1, 2, 3 });
        Assert.Throws<ArgumentNullException>(() => vertex.Value = null);
    }

    [Test]
    public void IsNeighbor_UsingVertex()
    {
        var vertex1 = new GraphVertex<int>(5);
        var vertex2 = new GraphVertex<int>(2);

        vertex1.Neighbors.Add(vertex2);
        Assert.IsTrue(vertex1.IsNeighbor(vertex2), "Vertex 1 should be neighbor of vertex 2");
    }

    [Test]
    public void IsNeighbor_UsingValue()
    {
        var vertex1 = new GraphVertex<int>(5);
        var vertex2 = new GraphVertex<int>(2);

        vertex1.Neighbors.Add(vertex2);
        Assert.IsTrue(vertex1.IsNeighbor(vertex2.Value), "Vertex 1 should be neighbor of vertex 2");
    }

    [Test]
    public void IsNotNeighbor_UsingVertex()
    {
        var vertex1 = new GraphVertex<int>(5);
        var vertex2 = new GraphVertex<int>(2);


        Assert.IsFalse(vertex1.IsNeighbor(vertex2), "Vertex 1 should be neighbor of vertex 2");
    }

    [Test]
    public void IsNotNeighbor_UsingValue()
    {
        var vertex1 = new GraphVertex<int>(5);
        var vertex2 = new GraphVertex<int>(2);


        Assert.IsFalse(vertex1.IsNeighbor(vertex2.Value), "Vertex 1 should be neighbor of vertex 2");
    }

    [Test]
    public void IsNeighborNull_UsingValue()
    {
        var vertex1 = new GraphVertex<int>(5);

        Assert.Throws<ArgumentNullException>(() => vertex1.IsNeighbor(null), "Vertex cannot be null");
    }

}
