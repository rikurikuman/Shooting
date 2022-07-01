using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeedFast = 100;
    public float moveSpeedSlow = 50;
    public PBullet bulletPrefab;
    
    float shotCT = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movespeed = 0;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = moveSpeedSlow;
        } else
        {
            movespeed = moveSpeedFast;
        }

        float move = movespeed * Time.deltaTime;

        Vector3 moveVec = new Vector3(0, 0, 0);

        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x++;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x--;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.z++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.z--;
        }

        pos += moveVec * move;
        if(pos.x < -22.5f)
        {
            pos.x = -22.5f;
        }
        if(pos.x > 22.5f)
        {
            pos.x = 22.5f;
        }
        if(pos.z < -10)
        {
            pos.z = -10;
        }
        if(pos.z > 30)
        {
            pos.z = 30;
        }
        transform.position = pos;

        shotCT -= Time.deltaTime;
        if (shotCT <= 0 && Input.GetKey(KeyCode.Space))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                bulletPrefab.Shot(transform.position, 6, 50);
                bulletPrefab.Shot(transform.position, 3, 50);
                bulletPrefab.Shot(transform.position, 0, 50);
                bulletPrefab.Shot(transform.position, -3, 50);
                bulletPrefab.Shot(transform.position, -6, 50);
            } else
            {
                bulletPrefab.Shot(transform.position, 20, 50);
                bulletPrefab.Shot(transform.position, 10, 50);
                bulletPrefab.Shot(transform.position, 0, 50);
                bulletPrefab.Shot(transform.position, -10, 50);
                bulletPrefab.Shot(transform.position, -20, 50);
            }
            
            shotCT = 0.1f;
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                Destroy(obj);
            }
        }

        transform.position = pos;
    }
}
