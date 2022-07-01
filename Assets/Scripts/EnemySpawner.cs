using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject hoge;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 1f;
            Instantiate(hoge, gameObject.transform.position + new Vector3(Random.Range(-20, 20), 0, 0), hoge.transform.rotation);
        }
    }
}
