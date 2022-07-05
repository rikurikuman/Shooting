using UnityEngine;

[System.Serializable]
public class EnemyTable : Serialize.TableBase<string, Enemy, EnemyPair> { }

[System.Serializable]
public class EnemyPair : Serialize.KeyAndValue<string, Enemy> { }

public class WaveManager : MonoBehaviour
{
    public EnemyTable enemys = new EnemyTable();

    public int nowWave = 0;
    public float timer = 0;
    public int counter = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (nowWave == 0)
        {
            if(timer > 3)
            {
                Enemy prefab = enemys.GetValue("Enemy01");

                prefab.Spawn(transform.position, 0, 25, 1);
                for(int i = 1; i <= counter; i++)
                {
                    prefab.Spawn(transform.position, 7.5f * i, 25, 1);
                    prefab.Spawn(transform.position, -7.5f * i, 25, 1);
                }

                IncreCounter();
            }

            if(counter >= 3)
            {
                IncreWave();
            }
        }
        else if(nowWave == 1)
        {
            if(timer > 3)
            {
                Enemy prefab = enemys.GetValue("Enemy02");

                prefab.Spawn(transform.position, 0, 25, 1);
                for (int i = 1; i <= counter; i++)
                {
                    prefab.Spawn(transform.position, 7.5f * i, 25, 1);
                    prefab.Spawn(transform.position, -7.5f * i, 25, 1);
                }

                IncreCounter();
            }

            if (counter >= 3)
            {
                IncreWave();
                timer = -3;
            }
        }
        else if (nowWave == 2)
        {
            if (timer > 0.5 && counter < 10)
            {
                Enemy prefab = enemys.GetValue("Enemy03");

                prefab.Spawn(transform.position, 10, 25, 1);

                IncreCounter();
            }

            if (counter >= 10)
            {
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    IncreWave();
                }
            }
        }
        else if (nowWave == 3)
        {
            if (timer > 0.5 && counter < 10)
            {
                Enemy prefab = enemys.GetValue("Enemy03");

                prefab.Spawn(transform.position, -10, 25, 1);

                IncreCounter();
            }

            if (counter >= 10)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    IncreWave();
                }
            }
        }
        else if (nowWave == 4)
        {
            if (timer > 3)
            {
                Enemy prefab = enemys.GetValue("Enemy04");
                

                if(counter >= 0)
                {
                    Enemy ene = prefab.Spawn(transform.position, 10, 25, 1);
                    ene.GetComponent<EnemyShot03>().angleSpeed = 19f;
                }
                if(counter >= 1)
                {
                    Enemy ene = prefab.Spawn(transform.position, -10, 25, 1);
                    ene.GetComponent<EnemyShot03>().angleSpeed = -19f;
                }

                IncreCounter();
            }

            if (counter >= 3)
            {
                IncreWave();
                timer = -2;
            }
        }
        else
        {
            if(timer > 1)
            {
                float rand = Random.Range(0.0f, 100.0f);
                if(rand <= 50)
                {
                    Enemy prefab = enemys.GetValue("Enemy02");
                    prefab.Spawn(transform.position, Random.Range(-15.0f, 15.0f), 25, 1);
                }
                else if(rand > 50 && rand <= 85)
                {
                    Enemy prefab = enemys.GetValue("Enemy03");
                    prefab.Spawn(transform.position, Random.Range(-15.0f, 15.0f), 25, 1);
                }
                else if(rand > 85)
                {
                    Enemy prefab = enemys.GetValue("Enemy04");
                    float startX = Random.Range(-15.0f, 15.0f);
                    Enemy ene = prefab.Spawn(transform.position, startX, 25, 1);
                    if(startX > 0)
                    {
                        ene.GetComponent<EnemyShot03>().angleSpeed = 19f;
                    } else
                    {
                        ene.GetComponent<EnemyShot03>().angleSpeed = -19f;
                    }
                }

                IncreCounter();
            }
        }
    }
    void IncreCounter()
    {
        counter++;
        timer = 0;
    }

    void IncreWave()
    {
        nowWave++;
        timer = 0;
        counter = 0;
    }
}
