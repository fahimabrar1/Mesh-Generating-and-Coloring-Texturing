using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MESH : MonoBehaviour
{
    Mesh mesh;

    public Vector3 contactPoint;

    MeshRenderer meshRenderer;
    Texture texture;
    Vector3[] verts;
    Color[] colors;
    Color color;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        
        verts = mesh.vertices;
        colors = new Color[verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            colors[i] = Color.black;
        }
        mesh.colors = colors;
        Debug.Log(colors.Length);
    }

    void Update()
    {
        int i = NearestVertexTo(contactPoint);
        colors[i] = Color.white;
        mesh.colors = colors;

        mesh.colors = colors;
        
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

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, Vector3.up, Color.green, 4, false);
            contactPoint = contact.point;
        }
    }


}
