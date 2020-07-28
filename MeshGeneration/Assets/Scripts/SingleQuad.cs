using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]

public class SingleQuad : MonoBehaviour
{
    public Material[] lightmap;
    MeshRenderer renderer;
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    List<int> triangles;
    List<int> triangles2;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeMeshData();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.subMeshCount = 2;
        Debug.Log(mesh.subMeshCount);

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.uv = uvs;

        mesh.subMeshCount = 2;

        mesh.SetTriangles(triangles,0);
        mesh.SetTriangles(triangles2,1);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        //renderer.sharedMaterial = new Material(Shader.Find("Standard"));
        if (lightmap.Length > 0)
            renderer.materials = lightmap;
    }

    private void MakeMeshData()
    {
        vertices = new Vector3[8];
        uvs = new Vector2[8];
        triangles = new List<int>();
        triangles2 = new List<int>();
        int v = 0;

        
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < 4; x++)
            {
                vertices[v] = new Vector3(x, 0, z);
                v++;
            }
        }

        v = 0;
        int t = 0;
        bool flip = true;

        

        for (int x = 0; x < 3; x++)
        {
            if(x<2)
            {
                /*triangles[t] = v;

                triangles[t + 1] = v + 4;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + 4;

                triangles[t + 5] = v + 5;*/
                
                triangles.Add(v);

                triangles.Add(v + 4);
                triangles.Add(v + 1);
                triangles.Add(v + 1);
                triangles.Add(v + 4);

                triangles.Add(v + 5);
            }
            else
            {
                /*t = 0;
                triangles2[t] = v;

                triangles2[t + 1] = v + 4;
                triangles2[t + 2] = v + 1;
                triangles2[t + 3] = v + 1;
                triangles2[t + 4] = v + 4;

                triangles2[t + 5] = v + 5;*/
                
                
                triangles2.Add(v);

                triangles2.Add(v + 4);
                triangles2.Add(v + 1);
                triangles2.Add(v + 1);
                triangles2.Add(v + 4);

                triangles2.Add(v + 5);
            }
            
            if(flip)
            {
                flip = false;

                uvs[v] = new Vector2(0, 0);
                uvs[v + 1] = new Vector2(0, 1);
                uvs[v + 4] = new Vector2(1, 0);
                uvs[v + 5] = new Vector2(1, 1);
            }
            else
            {
                flip = true;

                uvs[v] = new Vector2(0, 1);
                uvs[v + 1] = new Vector2(0, 0);
                uvs[v + 4] = new Vector2(1, 1);
                uvs[v + 5] = new Vector2(1, 0);
            }
           

            v += 1;
            t += 6;


           
        }
        
        
       

    }
}
