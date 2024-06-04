using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using pepe.graph;

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
        Assert.Throws<ArgumentNullException>(() => graph.AddVertex(null));
    }

    //[Test]
    //public void ContainsVertex()
    //{
    //    var graph = new Graph<int>();

    //    Assert.Throws<ArgumentNullException>(() => graph.AddVertex(null));
    //}

}
