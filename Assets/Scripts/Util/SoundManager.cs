using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SoundTable : Serialize.TableBase<string, AudioClip, SoundPair> {}

[System.Serializable]
public class SoundPair : Serialize.KeyAndValue<string, AudioClip> {}

public class SoundManager : MonoBehaviour
{
    //public IDictionary<string, GameObject> map = new Dictionary<string, GameObject>();
    public IDictionary<string, AudioClip> map = new Dictionary<string, AudioClip>();
    public SoundTable soundtable = new SoundTable();

    public static AudioSource source;
    public static SoundTable soundstable;

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

        soundstable = soundtable;
        source = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        source.volume = PlayerPrefs.GetFloat("SEVolume", 1.0f);
    }

    public static void Play(string soundname)
    {
        if(soundstable != null && soundstable.GetTable().ContainsKey(soundname))
        {
            AudioClip clip = soundstable.GetTable()[soundname];
            if (clip != null)
            {
                source.PlayOneShot(clip);
            }
        }
    }

    // 現在存在しているオブジェクト実体の記憶領域
    static SoundManager _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static SoundManager instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<SoundManager>()); }
    }

    void OnDestroy()
    {

        // ※破棄時に、登録した実体の解除を行なっている

        // 自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;

    }
}
