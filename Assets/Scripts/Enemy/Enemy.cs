using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 10;
    public int score = 300;

    public float startX = 0;
    public float startZ = 0;
    public float startEaseTime = 0;

    public Vector3 moveVec = new Vector3(0, 0, -10);

    bool spawning = true;
    float oldSpawnX = 0;
    float oldSpawnZ = 0;
    float spawntime = 0;

    public ParticleSystem explode;

    // Start is called before the first frame update
    void Start()
    {
        oldSpawnX = transform.position.x;
        oldSpawnZ = transform.position.z;
        spawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            spawntime += Time.deltaTime;
            float t = spawntime / startEaseTime;
            if(t > 1) {
                transform.position = new Vector3(startX, 0, startZ);
                spawning = false;
            } else
            {
                float ease = 1 - Mathf.Pow(1 - t, 4);

                Vector3 easePos = new Vector3(Mathf.Lerp(oldSpawnX, startX, ease), transform.position.y, Mathf.Lerp(oldSpawnZ, startZ, ease));
                transform.position = easePos;
            }
        }
        else
        {
            transform.GetComponent<Rigidbody>().velocity = moveVec;
        }

        if (hp <= 0)
        {
            GameManager.score += score;
            Instantiate(explode, transform.position, transform.rotation);
            SoundManager.Play("EnemyBreak");
            Destroy(gameObject);
        }

        if(!spawning)
        {
            Vector3 pos = transform.position;
            if (pos.x > 50 || pos.x < -50 || pos.z > 50 || pos.z < -50)
            {
                Destroy(gameObject);
            }
        }
    }

    public Enemy Spawn(Vector3 spawnPos, float startX, float startZ, float spawnTime)
    {
        return Spawn(spawnPos, startX, startZ, spawnTime, moveVec);
    }

    public Enemy Spawn(Vector3 spawnPos, float startX, float startZ, float spawnTime, Vector3 moveVec)
    {
        Enemy ene = Instantiate(this, spawnPos, transform.rotation);
        ene.startX = startX;
        ene.startZ = startZ;
        ene.startEaseTime = spawnTime;
        ene.moveVec = moveVec;
        return ene;
    }
}
