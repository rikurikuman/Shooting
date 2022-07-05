using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot02 : MonoBehaviour
{
    public EBullet bulletPrefab;
    float shotCT = 0;
    public float startCT = 1;
    public float shotCTSetting = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        shotCT = startCT;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 10)
        {
            shotCT -= Time.deltaTime;
            if (shotCT <= 0)
            {
                shotCT = shotCTSetting;

                GameObject player = GameObject.Find("Player");
                if(player == null)
                {
                    return;
                }
                Vector3 direction = player.transform.position - transform.position;
                if(direction.y == 0)
                {
                    direction.Normalize();
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    bulletPrefab.Shot(transform.position, rotation, 20);
                    bulletPrefab.Shot(transform.position, rotation * Quaternion.AngleAxis(15, Vector3.up), 20);
                    bulletPrefab.Shot(transform.position, rotation * Quaternion.AngleAxis(-15, Vector3.up), 20);

                    SoundManager.Play("Shot01");

                }
            }
        }
    }
}
