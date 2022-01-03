using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceHandler : MonoBehaviour
{
    [SerializeField] private float secondsPerMeter;
    [SerializeField] private SpawnManager spawnManager;
    private int distance;
    private float nextTime;
    private float time;
    public static Action SpeedUpAction;

    private void OnEnable()
    {
        EnergyManager.GameOverAction += CheckHighScore;
    }

    private void OnDisable()
    {
        EnergyManager.GameOverAction -= CheckHighScore;
    }

    void Update()
    {
        if (!EnergyManager.pause)
        {
            time += Time.deltaTime;
            if (nextTime <= time)
            {
                nextTime = secondsPerMeter - spawnManager.GetSpeed() + time;
                distance++;
                if (distance % 10 == 0)
                    SpeedUpAction.Invoke();
            }
        }
    }

    public int GetDistance()
    {
        return distance;
    }

    public void CheckHighScore()
    {
        if (distance > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", distance);
        }
    }
}
