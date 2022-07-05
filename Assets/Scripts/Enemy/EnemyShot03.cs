using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot03 : MonoBehaviour
{
    public EBullet bulletPrefab;
    float shotCT = 0;
    public float startCT = 1;
    public float shotCTSetting = 0.5f;

    float angle = 0;
    public float angleSpeed = 30;

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

               
                for (int i = 0; i < 3; i++)
                {
                    bulletPrefab.Shot(transform.position, angle, 10);
                    angle += 360 / 3.0f;
                }
                angle += angleSpeed;
                SoundManager.Play("Shot01");
            }
        }
    }
}
