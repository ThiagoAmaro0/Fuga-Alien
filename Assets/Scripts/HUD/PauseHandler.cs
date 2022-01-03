using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseHandler : MonoBehaviour
{
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
        TextMeshProUGUI text = EventSystem.current.currentSelectedGameObject
            .GetComponentInChildren<TextMeshProUGUI>();
        SoundManager.Mute();
        if (SoundManager.GetMute())
            text.text = "Ligar musica";
        else
            text.text = "Desligar musica";

    }
}
