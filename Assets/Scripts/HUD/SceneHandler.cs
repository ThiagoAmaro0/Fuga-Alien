using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        if(scene == "Menu")
        {
            SoundManager.Play("MenuMusic");
            EnergyManager.ContinueAction.Invoke();
        }
        SceneManager.LoadScene(scene);
    }

    public void PlaySound(string sound) => SoundManager.Play(sound);
    public void Quit() => Application.Quit();
}
