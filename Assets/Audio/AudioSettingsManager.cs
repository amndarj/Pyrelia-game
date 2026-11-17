
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer PyreliaMixer;

    private const string MASTER = "MasterVolume";
    private const string MUSIC = "MusicVolume";
    private const string SFX = "SFXVolume";

    private bool masterMuted;
    private bool musicMuted;
    private bool sfxMuted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        masterMuted = PlayerPrefs.GetInt("MasterMute", 0) == 1;
        musicMuted = PlayerPrefs.GetInt("MusicMute", 0) == 1;
        sfxMuted = PlayerPrefs.GetInt("SFXMute", 0) == 1;

        ApplyAllVolumes();
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MASTER, value);
        ApplyVolume(MASTER, value, masterMuted);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MUSIC, value);
        ApplyVolume(MUSIC, value, musicMuted);
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(SFX, value);
        ApplyVolume(SFX, value, sfxMuted);
    }

    public void ToggleMasterMute(bool muted)
    {
        masterMuted = muted;
        PlayerPrefs.SetInt("MasterMute", muted ? 1 : 0);
        ApplyVolume(MASTER, PlayerPrefs.GetFloat(MASTER, 1f), muted);
    }

    public void ToggleMusicMute(bool muted)
    {
        musicMuted = muted;
        PlayerPrefs.SetInt("MusicMute", muted ? 1 : 0);
        ApplyVolume(MUSIC, PlayerPrefs.GetFloat(MUSIC, 1f), muted);
    }

    public void ToggleSFXMute(bool muted)
    {
        sfxMuted = muted;
        PlayerPrefs.SetInt("SFXMute", muted ? 1 : 0);
        ApplyVolume(SFX, PlayerPrefs.GetFloat(SFX, 1f), muted);
    }

    private void ApplyAllVolumes()
    {
        ApplyVolume(MASTER, PlayerPrefs.GetFloat(MASTER, 1f), masterMuted);
        ApplyVolume(MUSIC, PlayerPrefs.GetFloat(MUSIC, 1f), musicMuted);
        ApplyVolume(SFX, PlayerPrefs.GetFloat(SFX, 1f), sfxMuted);
    }

    private void ApplyVolume(string parameter, float value, bool muted)
    {
        float db = muted || value <= 0.0001f
            ? -80f
            : Mathf.Log10(value) * 20f;

        PyreliaMixer.SetFloat(parameter, db);
    }
}
