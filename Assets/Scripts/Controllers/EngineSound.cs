using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [Header("Engine Sound Config")]
    [SerializeField] private float picthDecay;
    [SerializeField] private float pitchIncrease;
    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;
    [SerializeField] private float volumeDecay;
    [SerializeField] private float volumeIncrease;
    [SerializeField] private float volumeMin;
    [SerializeField] private float volumeMax;

    [SerializeField] private AudioSource audioSource;

    private Tween volumeTween;
    private Tween pitchTween;

    private bool isUp;

    private void OnEnable()
    {
        EnergyManager.PauseAction += Pause;
        EnergyManager.ContinueAction += Continue;
        EnergyManager.GameOverAction += Pause;
    }

    private void OnDisable()
    {
        EnergyManager.PauseAction -= Pause;
        EnergyManager.ContinueAction -= Continue;
        EnergyManager.GameOverAction -= Pause;
    }

    private void Pause()
    {
        audioSource.Pause();
    }
    private void Continue()
    {
        audioSource.Play();
    }

    public void SpeedUp()
    {
        if (isUp)
            return;
        isUp = true;

        volumeTween?.Kill();
        pitchTween?.Kill();

        float volume = audioSource.volume;
        float pitch = audioSource.pitch;

        volumeTween = audioSource.DOFade(volumeMax, volumeIncrease * 1 - volume);
        pitchTween = audioSource.DOPitch(pitchMax, pitchIncrease * 1 - pitch);
    }

    public void SpeedDown()
    {
        if (!isUp)
            return;
        isUp = false;

        volumeTween?.Kill();
        pitchTween?.Kill();

        float volume = audioSource.volume;
        float pitch = audioSource.pitch;

        volumeTween = audioSource.DOFade(volumeMin, volumeDecay * volume);
        pitchTween = audioSource.DOPitch(pitchMin, picthDecay * pitch);
    }
}
