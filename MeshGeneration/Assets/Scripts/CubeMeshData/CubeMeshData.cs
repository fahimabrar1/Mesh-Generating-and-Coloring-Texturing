using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData 
{
    public static Vector3[] vertices =
    {
        new Vector3(1,  1,  1),
        new Vector3(-1, 1,  1),
        new Vector3(-1,  -1, 1),
        new Vector3(1, -1, 1),

        new Vector3(-1,  1,  -1),
        new Vector3(1, 1,  -1),
        new Vector3(1,  -1, -1),
        new Vector3(-1, -1, -1)
 
    };

    public static int[][] facetriangles =
    {
        new int[] {0,  1,  2,  3},
        new int[] {4,   5,  6,  7},
        new int[] {1,   0,  5,  4},
        new int[] {0,   3,  6,  5},
        new int[] {1,   4,  7,  2},
        new int[] {7,   6,  3,  2},
    };

    public static Vector3[] facevertices(int dir, float cubeScale, Vector3 cubePos)
    {
        Vector3[] fv = new Vector3[4];
        for(int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[facetriangles[dir][i]]*cubeScale + cubePos;
        }
        return fv;
    }
    public static Vector3[] facevertices(Direction dir, float cubeScale, Vector3 cubePos)
    {
        return facevertices((int)dir,cubeScale,cubePos);
    }
}
