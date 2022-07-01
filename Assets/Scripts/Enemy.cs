using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -10) * Time.deltaTime, ForceMode.VelocityChange);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 pos = transform.position;
        if (pos.x > 50 || pos.x < -50 || pos.z > 50 || pos.z < -50)
        {
            Destroy(gameObject);
        }
    }
}
