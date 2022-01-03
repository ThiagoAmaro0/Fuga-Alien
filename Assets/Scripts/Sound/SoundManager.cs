using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixerGroup defaultMixer;
    [SerializeField] private AudioMixerGroup zoneMixer;
    public static SoundManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Play("MenuMusic");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EnergyManager.PauseAction += EnterZone;
        EnergyManager.ContinueAction += ExitZone;
    }

    private void OnDisable()
    {
        EnergyManager.PauseAction -= EnterZone;
        EnergyManager.ContinueAction -= ExitZone;
    }

    public static void Play(string sound)
    {
        if (!instance)
            return;

        foreach(Sound s in instance.sounds)
        {
            if(s.name == sound)
            {
                instance.StartSound(s);
            }
        }
    }

    private void StartSound(Sound sound)
    {
        if (sound.Loop)
        {
            if (sound.Clip == musicSource.clip)
                return;
            musicSource.volume = sound.Volume;
            musicSource.loop = sound.Loop;
            musicSource.clip = sound.Clip;
            musicSource.Play();
        }
        else
        {
            if (sound.Solo)
            {
                audioSource.volume = sound.Volume;
                audioSource.clip = sound.Clip;
                audioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(sound.Clip);
            }
        }
    }
    public static void Mute()
    {
        instance.musicSource.mute = !instance.musicSource.mute;
    }
    public static bool GetMute()
    {
        return instance.musicSource.mute;
    }

    public void EnterZone()
    {
        musicSource.outputAudioMixerGroup = zoneMixer;
    }
    public void ExitZone()
    {
        musicSource.outputAudioMixerGroup = defaultMixer;
    }
}
