using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool go = false;

    public static int score = 0;
    public static int graze = 0;

    public TextMesh scoreText;
    public TextMesh grazeText;

    public Canvas finishCanvas;
    public Text resultScoreText;

    void Update()
    {
        scoreText.text = score.ToString();
        grazeText.text = graze.ToString();

        if(GameObject.Find("Player") == null)
        {
            finishCanvas.gameObject.SetActive(true);
            resultScoreText.text = "Score: " + score;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                BackTitle();
            }
        }

        if(!go)
        {
            BGMManager.Play("Game");
        }
    }

    public void BackTitle()
    {
        if (!go)
        {
            async UniTask task()
            {
                go = true;
                await Shutter.Close();
                BGMManager.Stop(1);
                await UniTask.Delay(1000);
                await SceneManager.LoadSceneAsync("TitleScene");
                await Shutter.Open();
                go = false;
            }
            _ = task();
        }
    }
}
