using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triancles;

    public float scale = 1f;
    float adscale;

    public float posX, posY, posZ;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adscale = scale * 0.5f;

    }

    private void Start()
    {
        MakeCube(adscale, new Vector3(posX*scale,posY*scale,posZ*scale));
        UpdateMesh();
    }

    private void MakeCube(float CubeScale, Vector3 cubePos)
    {
        vertices = new List<Vector3>();
        triancles = new List<int>();

        for (int i =0; i <6; i++)
        {
            MakeFace(i,CubeScale,cubePos);
        }
        
    }

    private void MakeFace(int i, float cubeScale, Vector3 cubePos)
    {
        vertices.AddRange(CubeMeshData.facevertices(i, cubeScale,cubePos) );
        triancles.Add(vertices.Count-4 + 0);
        triancles.Add(vertices.Count - 4 + 1);
        triancles.Add(vertices.Count - 4 + 2);
        triancles.Add(vertices.Count - 4 + 0);
        triancles.Add(vertices.Count - 4 + 2);
        triancles.Add(vertices.Count - 4 + 3);
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triancles.ToArray();
        mesh.RecalculateNormals();
    }
}
