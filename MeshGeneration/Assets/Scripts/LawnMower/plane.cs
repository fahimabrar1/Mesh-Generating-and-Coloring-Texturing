using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer),typeof(MeshCollider))]

public class plane : MonoBehaviour
{
    Mesh mesh;
    MeshRenderer MeshRenderer;
    MeshCollider MeshCollider;
    Vector3[] vertices;
    Color[] colors;

    int[] triangles;

    //grid settings
    public float cellsize=1;
    public Vector3 gridoffset;
    public int gridesize=1;


    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer = GetComponent<MeshRenderer>();
        MeshCollider = GetComponent<MeshCollider>();
    }
    private void Start()
    {
        MakeContigiousProceduralGrid();
        UpdateMesh();
        MeshCollider.sharedMesh=mesh;
      //  MeshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
    }

    private void MakeDiscreteProceduralGrid()
    {
        //set array size
        vertices = new Vector3[gridesize * gridesize * 4];
        triangles = new int[gridesize * gridesize * 6];

        //set trackers integers
        int v = 0;
        int t = 0;
        //set vertex offset
        float vertecoffset = cellsize * 0.5f;

        for (int x = 0; x < gridesize; x++)
        {
            for (int z = 0; z < gridesize; z++)
            {
                Vector3 celloffset = new Vector3(x * cellsize, 0, z * cellsize);

                //populate the vertices and triangles arrays
                vertices[v] = new Vector3(-vertecoffset, 0, -vertecoffset) + celloffset + gridoffset;
                vertices[v + 1] = new Vector3(-vertecoffset, 0, vertecoffset) + celloffset + gridoffset;
                vertices[v + 2] = new Vector3(vertecoffset, 0, -vertecoffset) + celloffset + gridoffset;
                vertices[v + 3] = new Vector3(vertecoffset, 0, vertecoffset) + celloffset + gridoffset;

                triangles[t] = v;

                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;

                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }

    private void MakeContigiousProceduralGrid()
    {
        //set array size
        vertices = new Vector3[(gridesize + 1) * (gridesize + 1)];
        triangles = new int[gridesize * gridesize * 6];

        //set trackers integers
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertecoffset = cellsize * 0.5f ;
       

        //create vertex grid
        for (int x = 0; x <= gridesize; x++)
        {
            for (int z = 0; z <= gridesize; z++)
            {
                vertices[v] = new Vector3((x * cellsize) - gridesize*0.5f*cellsize , 0 + gridoffset.y, (z * cellsize) - cellsize*gridesize * 0.5f ) ;
                v++;
            }
        }
        //reset vertex tracekr
        v = 0;

        //Settings cell triangles
        for (int x = 0; x < gridesize; x++)
        {
            for (int z = 0; z < gridesize; z++)
            {
                triangles[t] = v;

                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridesize + 1);

                triangles[t + 5] = v + (gridesize + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    private void UpdateMesh()
    {
        
        mesh.Clear();
        colors = new Color[vertices.Length];
        Debug.Log("Colors: "+colors.Length);
        Debug.Log("vertices: "+vertices.Length);
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.green;
        }
        mesh.vertices = vertices;

        mesh.SetColors(colors);

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
