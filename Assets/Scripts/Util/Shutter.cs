using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shutter : MonoBehaviour
{
    public GameObject shutter;
    private static bool isClose = false;
    private static float width = 0;
    private static bool incompleteAction = false;

    private void Start()
    {
    }

    void Update()
    {
        if(isClose)
        {
            if (width < 1920)
            {
                width += 1920 * Time.deltaTime * 5;
                if (width >= 1920)
                {
                    width = 1920;
                    incompleteAction = false;
                }
                shutter.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920 - width, 0);
                shutter.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 2000);
            }
        } else
        {
            if (width > 0)
            {
                width -= 1920 * Time.deltaTime * 5;
                if (width <= 0)
                {
                    width = 0;
                    incompleteAction = false;
                }
                shutter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                shutter.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 2000);
            }
        }
    }

    public static async UniTask Open()
    {
        isClose = false;
        incompleteAction = true;
        SoundManager.Play("ShutterOpen");

        while(true)
        {
            if(!incompleteAction)
            {
                break;
            }

            await UniTask.DelayFrame(1);
        }
    }

    public static async UniTask Close()
    {
        isClose = true;
        incompleteAction = true;
        SoundManager.Play("ShutterClose");

        while (true)
        {
            if(!incompleteAction)
            {
                break;
            }

            await UniTask.DelayFrame(1);
        }
    }

    //DontDestroyOnLoad用
    // 現在存在しているオブジェクト実体の記憶領域
    static Shutter _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static Shutter instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<Shutter>()); }
    }

    void Awake()
    {
        // ※オブジェクトが重複していたらここで破棄される

        // 自身がインスタンスでなければ自滅
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        // 以降破棄しない
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        // ※破棄時に、登録した実体の解除を行なっている

        // 自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;
    }
}
