using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatorMeshSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * 0.3f;




        foreach (var i in vertexPositionsGrid(new Vector3(1.5f, 0, 1.5f), 13.5f, 1))
        { Instantiate(sphere, i, Quaternion.identity); }
    }

    List<Vector3> vertexPositionsGrid(Vector3 startingPosition, float maxPosition, float step)
    {
        List<Vector3> vertexPositions = new List<Vector3>();

        for (float x = startingPosition.x; x <= maxPosition; x += step)
        {
            for (float y = startingPosition.z; y <= maxPosition; y += step)
            {
                vertexPositions.Add(new Vector3(x, 0, y));
            }
        }

        return vertexPositions;
    }


}
