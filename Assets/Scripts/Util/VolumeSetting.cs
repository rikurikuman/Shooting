using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SESlider;
    // Start is called before the first frame update
    void Start()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        SESlider.value = PlayerPrefs.GetFloat("SEVolume", 0.5f);
        SESlider.onValueChanged.AddListener( PlayTestSE );
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.SetFloat("SEVolume", SESlider.value);
    }

    public void PlayTestSE(float a)
    {
        SoundManager.Play("CursorPush");
    }
}
