using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpinner : MonoBehaviour
{

    Transform transform;

    float SpinRadious=1;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    float angle = 0;
    float speed = (2 * Mathf.PI)*1; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius = 1;
    void FixedUpdate()
    {
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
       float x = Mathf.Cos(angle) * radius;
       float z = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x,transform.position.y,z);
    }
}
