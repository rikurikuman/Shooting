using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BGMTable : Serialize.TableBase<string, AudioClip, BGMPair> {}

[System.Serializable]
public class BGMPair : Serialize.KeyAndValue<string, AudioClip> {}

public class BGMManager : MonoBehaviour
{
    //public IDictionary<string, GameObject> map = new Dictionary<string, GameObject>();
    public IDictionary<string, AudioClip> map = new Dictionary<string, AudioClip>();
    public BGMTable bgmtable = new BGMTable();

    private static AudioSource source;
    private static BGMTable bgmstable;
    private static string nowplaying = null;
    private static double fadeouttime = 0;
    private static double fadedelta = 0;
    private static bool fadeouting = false;

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

        bgmstable = bgmtable;
        source = gameObject.GetComponent<AudioSource>();
    }


    private void Update()
    {
        if(fadeouting)
        {
            fadedelta += Time.unscaledDeltaTime;
            if(fadedelta >= fadeouttime)
            {
                fadedelta = fadeouttime;
                fadeouting = false;
                source.Stop();
            }
            source.volume = (float)(GetVolume() - fadedelta / fadeouttime);
        }
        else
        {
            source.volume = GetVolume();
        }
    }

    public static string GetNowPlaying()
    {
        return nowplaying;
    }

    public static float GetVolume()
    {
        return 0.5f * PlayerPrefs.GetFloat("BGMVolume", 0.5f);
    }

    public static void Play(string soundname)
    {
        if (bgmstable != null && bgmstable.GetTable().ContainsKey(soundname))
        {
            AudioClip clip = bgmstable.GetTable()[soundname];
            if (clip != null && (nowplaying == null || (nowplaying != null && nowplaying != soundname)))
            {
                source.Stop();
                fadeouting = false;
                source.volume = GetVolume();
                source.clip = clip;
                nowplaying = soundname;
                source.loop = true;
                source.Play();
            }
        }
    }

    public static void Stop(double time)
    {
        nowplaying = null;
        if(time <= 0)
        {
            source.Stop();
        }
        else
        {
            fadeouting = true;
            fadeouttime = time;
            fadedelta = 0;
        }
    }

    // 現在存在しているオブジェクト実体の記憶領域
    static BGMManager _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static BGMManager instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<BGMManager>()); }
    }

    void OnDestroy()
    {

        // ※破棄時に、登録した実体の解除を行なっている

        // 自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;

    }
}
