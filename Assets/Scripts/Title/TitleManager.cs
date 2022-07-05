using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static bool go = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
        if (!go)
        {
            BGMManager.Play("Title");
        }
    }

    public void GameStart()
    {
        if(!go)
        {
            async UniTask task()
            {
                go = true;
                await Shutter.Close();
                BGMManager.Stop(1);
                await UniTask.Delay(1000);
                GameManager.score = 0;
                GameManager.graze = 0;
                await SceneManager.LoadSceneAsync("GameScene");
                await Shutter.Open();
                go = false;
            }
            _ = task();
        }
    }
}
