using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangelDetector : MonoBehaviour
{
    public Camera cam;
    RaycastHit Hit;
    void Update()
    {
        RaycastHit hit;
        /*if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;*/
        if (!Physics.SphereCast(cam.ScreenPointToRay(Input.mousePosition), 2, out hit))
            return;

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
            return;
        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Debug.Log("Hit Triangle Index" + hit.triangleIndex * 3);
        Debug.Log("Triangle hit" + triangles[hit.triangleIndex]);
        Debug.Log("Vertices hit" + vertices[triangles[hit.triangleIndex * 3]]);
        Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

        Transform hitTransform = hit.collider.transform;
        p0 = hitTransform.TransformPoint(p0);
        p1 = hitTransform.TransformPoint(p1);
        p2 = hitTransform.TransformPoint(p2);

        Debug.DrawLine(p0, p1, Color.green);
        Debug.DrawLine(p1, p2, Color.green);
        Debug.DrawLine(p2, p0, Color.green);

        Hit = hit;


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Debug.DrawLine(cam.transform.position, Hit.point, Color.red);
        Gizmos.DrawWireSphere(Hit.point, 2f);
    }
}
