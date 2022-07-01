using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        transform.GetComponent<Rigidbody>().velocity = transform.up * speed;

        if(pos.x > 50 || pos.x < -50 || pos.z > 50 || pos.z < -50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().hp -= 1;
            Destroy(gameObject);
        }
    }

    public void Shot(Vector3 pos, float angle, float speed)
    {
        PBullet bul = Instantiate(this);
        bul.speed = speed;
        bul.transform.position = pos;
        bul.transform.rotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
