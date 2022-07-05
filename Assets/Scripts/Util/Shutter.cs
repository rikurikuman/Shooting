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

    //DontDestroyOnLoad�p
    // ���ݑ��݂��Ă���I�u�W�F�N�g���̂̋L���̈�
    static Shutter _instance = null;

    // �I�u�W�F�N�g���̂̎Q�Ɓi�����Q�Ǝ��A���̂̓o�^���s���j
    static Shutter instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<Shutter>()); }
    }

    void Awake()
    {
        // ���I�u�W�F�N�g���d�����Ă����炱���Ŕj�������

        // ���g���C���X�^���X�łȂ���Ύ���
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        // �ȍ~�j�����Ȃ�
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        // ���j�����ɁA�o�^�������̂̉������s�Ȃ��Ă���

        // ���g���C���X�^���X�Ȃ�o�^������
        if (this == instance) _instance = null;
    }
}
