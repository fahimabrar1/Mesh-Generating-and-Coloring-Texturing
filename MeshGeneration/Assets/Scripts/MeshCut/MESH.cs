using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MESH : MonoBehaviour
{
    Mesh mesh;

    public Vector3 contactPoint;
    public Material[] lightmap;


    MeshRenderer meshRenderer;
    Vector3[] verts;
    Color[] colors;
    List<int> triangles;
    List<int> triangles1;
    List<int> getpoints;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();

        verts = mesh.vertices;
        triangles = new List<int>();
        triangles1 =new List<int>();

        getpoints =new List<int>();

        

       triangles = mesh.GetTriangles(0).ToList();
        Debug.Log("Triangles in submesh 0:   "+triangles.Count());
        mesh.subMeshCount = 2;

        AddInitialColorToMesh();

        /*
                Debug.Log("Vertices: " + verts.Length);
                Debug.Log("triangles: " + triangles.Length);
                Debug.Log("triangles    0 : " + triangles[0]);
                Debug.Log("triangles    1 : " + triangles[1]);
                Debug.Log("triangles    2 : " + triangles[2]);
                Debug.Log("triangles    3 : " + triangles[3]);
                Debug.Log("triangles    4 : " + triangles[4]);
                Debug.Log("triangles    5 : " + triangles[5]);
                Debug.Log("triangles    6 : " + triangles[6]);
                Debug.Log("triangles    7 : " + triangles[7]);
                Debug.Log("triangles    8 : " + triangles[8]);
                Debug.Log("triangles    9 : " + triangles[9]);
                Debug.Log("triangles    10 : " + triangles[10]);
                Debug.Log("triangles    11 : " + triangles[11]);
       */
    }

    
    void Update()
    {
        int j = NearestVertexTo(contactPoint);
        //  Debug.Log("Vertex number: " + j);
        
            if (j >= 0 && !getpoints.Contains(j))
            {
                getpoints.Add(j);

                for (int i = 0; i < triangles.Count(); i++)
                {
                    if (triangles[i] == j)
                    {
                        triangles1.Add(j);
                        triangles1.Add(j + 11);
                        triangles1.Add(j + 11 + 1);

                        triangles1.Add(j);
                        triangles1.Add(j + 11 + 1);
                        triangles1.Add(j + 1);

                    }
                }

            }


        mesh.SetVertices(verts);
        mesh.SetTriangles(triangles, 0);
        mesh.SetTriangles(triangles1, 1);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        if (lightmap.Length > 0)
        {
            meshRenderer.materials = lightmap;
        }

        AddColorToMesh(j);

    }


    int NearestVertexTo(Vector3 point)
    {
       // point = transform.InverseTransformPoint(point);

        float minDistanceSqr = Mathf.Infinity;
        int nearestVertex = -1;

        // scan all vertices to find nearest
        for (int i = 0; i < verts.Length; i++)
        {
            float distSqr = (point - verts[i]).sqrMagnitude;

            if (distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
                nearestVertex = i;
            }
        }
        return nearestVertex;
    }


    private void AddColorToMesh(int i)
    {
       colors[i] = Color.white;
       mesh.colors = colors;

       mesh.colors = colors;
    }


    private void AddInitialColorToMesh()
    {
        colors = new Color[verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            colors[i] = Color.black;
        }
        mesh.colors = colors;
        Debug.Log(colors.Length);
    }




    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, Vector3.up, Color.green, 4, false);
            contactPoint = contact.point;
        }
    }


}
