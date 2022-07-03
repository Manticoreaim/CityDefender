using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetingsSkript : MonoBehaviour
{

    [Header("Button for save settings to PlayerPrefs")]
    [SerializeField] private Button ButtonSaveing;

    [Header("Sliders Setings")]
    [SerializeField] private Slider SliderMusicSoundValue;
    [SerializeField] private Slider SliderEffectSoundValue;

    [Header("Keys to PlayerPrefs")]
    [SerializeField] private string KeyMusicSoundValue = "musicvalue";
    [SerializeField] private string KeyEffectSoundValue = "effectvalue";

    [Header("SoundPlayer")]
    [SerializeField] private AudioSource MusicPlayer;

    private float MusicSoundValue;
    private float EffectSoundValue;

    // Start is called before the first frame update
    void Start()
    {
        SetSetings();
        InitializationButtonAndSliders();

    }

    private void InitializationButtonAndSliders()
    {
        ButtonSaveing.onClick.AddListener(SaveSetingsToPlayerPrefs);

        SliderMusicSoundValue.onValueChanged.AddListener(SetMusicSaundValue);
        SliderEffectSoundValue.onValueChanged.AddListener(SetEffectSaundValue);
    }

    private void SetSetings()
    {
        if(PlayerPrefs.HasKey(KeyMusicSoundValue))
        {
            MusicSoundValue = PlayerPrefs.GetFloat(KeyMusicSoundValue);
            SliderMusicSoundValue.value = MusicSoundValue;
        }
        else
        {
            MusicSoundValue = 1f;
            PlayerPrefs.SetFloat(KeyMusicSoundValue, MusicSoundValue);
            SliderMusicSoundValue.value = MusicSoundValue;
        }
        
        if(PlayerPrefs.HasKey(KeyEffectSoundValue))
        {
            EffectSoundValue = PlayerPrefs.GetFloat(KeyEffectSoundValue);
            SliderEffectSoundValue.value = EffectSoundValue;
        }
        else
        {
            EffectSoundValue = 1f;
            PlayerPrefs.SetFloat(KeyEffectSoundValue, EffectSoundValue);
            SliderEffectSoundValue.value = EffectSoundValue;
        }
    }

    private void SaveSetingsToPlayerPrefs()
    {
        PlayerPrefs.SetFloat(KeyMusicSoundValue, MusicSoundValue);
        PlayerPrefs.SetFloat(KeyEffectSoundValue, EffectSoundValue);
    }


    private void SetMusicSaundValue(float Vol)
    {
        MusicSoundValue = Vol;
        MusicPlayer.volume = MusicSoundValue;
    }
    private void SetEffectSaundValue(float Vol)
    {
        EffectSoundValue = Vol;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(MusicSoundValue);
        Debug.Log(EffectSoundValue);
    }
}
