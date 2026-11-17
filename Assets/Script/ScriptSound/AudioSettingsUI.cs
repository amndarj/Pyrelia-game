
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public Button masterMute;
    public Button musicMute;
    public Button sfxMute;

    private AudioSettingsManager manager;

    private bool masterMuted;
    private bool musicMuted;
    private bool sfxMuted;

    private void Start()
    {
        manager = AudioSettingsManager.Instance;

        if (manager == null)
        {
            Debug.LogError("AudioSettingsManager tidak ditemukan!");
            return;
        }

        masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MasterVolume", 1f));
        musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVolume", 1f));
        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume", 1f));

        masterMuted = PlayerPrefs.GetInt("MasterMute", 0) == 1;
        musicMuted = PlayerPrefs.GetInt("MusicMute", 0) == 1;
        sfxMuted = PlayerPrefs.GetInt("SFXMute", 0) == 1;

        masterSlider.onValueChanged.AddListener(manager.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(manager.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(manager.SetSFXVolume);

        masterMute.onClick.AddListener(() =>
        {
            masterMuted = !masterMuted;
            manager.ToggleMasterMute(masterMuted);
        });

        musicMute.onClick.AddListener(() =>
        {
            musicMuted = !musicMuted;
            manager.ToggleMusicMute(musicMuted);
        });

        sfxMute.onClick.AddListener(() =>
        {
            sfxMuted = !sfxMuted;
            manager.ToggleSFXMute(sfxMuted);
        });
    }
}
