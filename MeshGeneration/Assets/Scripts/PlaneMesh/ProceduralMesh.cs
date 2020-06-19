using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ProceduralMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        MakeMeshData();
        CreateMesh();
    }

    private void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0, YVALUE.ins.yvalue, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) , new Vector3(1,0,1) };
        triangles = new int[] { 0, 1, 2 , 2 ,1 ,3  };
    }
}
