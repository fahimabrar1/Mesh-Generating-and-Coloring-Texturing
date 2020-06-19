using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YVALUE : MonoBehaviour
{

    public float yvalue;
    public static YVALUE ins;

    private void Awake()
    {
        ins = this;
    }

}
