using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeedFast = 100;
    public float moveSpeedSlow = 50;
    public PBullet bulletPrefab;

    public ParticleSystem explode;

    public TextMesh lifeText;
    public TextMesh bombText;

    public int life = 3;
    public int bomb = 2;
    public float invincibleTime = 0;
    public float respawnTime = 0;

    float shotCT = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        lifeText.text = life.ToString();
        bombText.text = bomb.ToString();

        if (respawnTime > 0)
        {
            transform.position = new Vector3(0, 100000, -8.5f);
        } else
        {
            Vector3 _pos = transform.position;
            _pos.y = 0;
            transform.position = _pos;
        }

        invincibleTime -= Time.deltaTime;
        respawnTime -= Time.deltaTime;

        float movespeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = moveSpeedSlow;
        } else
        {
            movespeed = moveSpeedFast;
        }

        float move = movespeed * Time.deltaTime;

        Vector3 moveVec = new Vector3(0, 0, 0);

        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x++;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
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

            SoundManager.Play("PlayerShot");
            shotCT = 0.1f;
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            if(bomb > 0)
            {
                SoundManager.Play("PlayerBomb");
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EnemyBullet"))
                {
                    Destroy(obj);
                }
                bomb--;
            }
        }

        transform.position = pos;
    }

    public void Death()
    {
        if(invincibleTime > 0)
        {
            return;
        }
        Instantiate(explode, transform.position, transform.rotation);
        SoundManager.Play("Explode");
        
        if (life == 0)
        {
            life = 0;
            bomb = 0;
            Update();
            Destroy(gameObject);
        }
        else
        {
            life--;
            bomb = 2;
            invincibleTime = 5;
            respawnTime = 1;
        }
    }
}
