using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class VoxelRendering : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triancles;

    public float scale = 1f;
    float adscale;


    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adscale = scale * 0.5f;

    }

    private void Start()
    {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh();
    }

    private void GenerateVoxelMesh(VoxelData voxelData)
    {
        vertices = new List<Vector3>();
        triancles = new List<int>();
        
        for (int x = 0; x < voxelData.Width; x++)
        {
            for (int z = 0; z < voxelData.Depth; z++)
            {   
                if (voxelData.GetCell(x,z)==0)
                {
                    continue;
                }
                MakeCube(adscale, new Vector3((x * scale), 0, (z * scale)), x ,z , voxelData);
            }
        }
    }

    private void MakeCube(float CubeScale, Vector3 cubePos , int x , int z , VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if (data.GetNeighbour(x,z,(Direction)i)==0
                )
            {
                MakeFace((Direction)i, CubeScale, cubePos);
            }
        }
    }

    private void MakeFace(Direction i, float cubeScale, Vector3 cubePos)
    {
        vertices.AddRange(CubeMeshData.facevertices(i, cubeScale, cubePos));
        triancles.Add(vertices.Count - 4 + 0);
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
