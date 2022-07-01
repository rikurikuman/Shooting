using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public float distance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        pos += -transform.forward * distance;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
