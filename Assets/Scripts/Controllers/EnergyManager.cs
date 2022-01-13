using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnergyManager : MonoBehaviour
{
    [Header("Energy Settings")]
    [SerializeField] private float initialEnergy;
    [SerializeField] private float energyCost;

    [Header("EnergyUp Spawn Settings")]
    [SerializeField] private GameObject energyUpPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    private GameObject spawnedEnergyUp;
    private float nextTime;
    private float energy;
    private float time;

    public static bool pause;
    public static Action PauseAction;
    public static Action ContinueAction;
    public static Action GameOverAction;
    public static Action<float> EnergyUpAction;

    private void Start()
    {
        ContinueAction.Invoke();
        energy = initialEnergy;
        nextTime = time + maxDelay;
    }

    private void OnEnable()
    {
        PauseAction += Pause;
        ContinueAction += Continue;
        EnergyUpAction += EnergyUp;
    }

    private void OnDisable()
    {
        PauseAction -= Pause;
        ContinueAction -= Continue;
        EnergyUpAction -= EnergyUp;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!pause)
        {
            energy -= energyCost * Time.deltaTime;
            time += Time.deltaTime;
            if (energy <= 0 && !pause)
                GameOverAction.Invoke();

            if (nextTime <= time && !pause)
            {
                SpawnEnergyUp();
            }
        }
    }

    private void SpawnEnergyUp()
    {
        nextTime = time + Random.Range(minDelay,maxDelay);

        if (spawnedEnergyUp)
        {
            Obstacle obstacle = spawnedEnergyUp.GetComponent<Obstacle>();
            obstacle.Initialize(speed);
            spawnedEnergyUp.SetActive(true);
        }
        else
        {
            spawnedEnergyUp = Instantiate(energyUpPrefab, new Vector3(11, Random.Range(-4f, 4f), 0), Quaternion.identity);
            Obstacle obstacle = spawnedEnergyUp.GetComponent<Obstacle>();
            obstacle.Initialize(speed);
        }
    }

    private void Pause()
    {
        //Time.timeScale = 0;
        pause = true;
    }

    private void Continue()
    {
        pause = false;
    }

    private void EnergyUp(float value)
    {
        energy += value;
        energy = Mathf.Clamp(energy, 0, 100);
        SoundManager.PlayOnShot("EnergyUp", Random.Range(0.75f, 1.25f), Random.Range(0.75f, 1));
    }

    public float GetEnergy()
    {
        return energy;
    }
    
    public float GetEnergyCost()
    {
        return energyCost;
    }



    
}
