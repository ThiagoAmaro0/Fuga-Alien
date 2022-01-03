using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI muteText;
    public void Pause()
    {
        EnergyManager.PauseAction.Invoke();
    }
    public void Resume()
    {
        EnergyManager.ContinueAction.Invoke();
    }
    public void Mute()
    {
        SoundManager.Mute();
        if (SoundManager.GetMute())
            muteText.text = "Ligar musica";
        else
            muteText.text = "Desligar musica";

    }
}
