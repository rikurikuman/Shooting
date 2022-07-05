using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot01 : MonoBehaviour
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
        if(transform.position.z > 10)
        {
            shotCT -= Time.deltaTime;
            if (shotCT <= 0)
            {
                shotCT = shotCTSetting;

                float angle = 0;
                for (int i = 0; i < 12; i++)
                {
                    bulletPrefab.Shot(transform.position, angle, 10);
                    angle += 360 / 12.0f;
                }
                SoundManager.Play("Shot01");
            }
        }
    }
}
