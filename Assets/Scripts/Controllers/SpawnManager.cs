using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [Header("Speed Config")]
    [SerializeField] private float speed;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float maxSpeed;

    [Header("Delay Config")]
    [SerializeField] private float delay;
    [SerializeField] private float decreaseDelay;
    [SerializeField] private float minDelay;

    private List<Obstacle> spawnedObstacles;
    private bool end;
    private float nextTime;
    private float time;

    private void Start()
    {
        spawnedObstacles = new List<Obstacle>();
    }

    private void OnEnable()
    {
        DistanceHandler.SpeedUpAction += SpeedUp;
        EnergyManager.PauseAction += Stop;
        EnergyManager.ContinueAction += Continue;
    }

    private void OnDisable()
    {
        DistanceHandler.SpeedUpAction -= SpeedUp;
        EnergyManager.PauseAction -= Stop;
        EnergyManager.ContinueAction -= Continue;
    }

    private void Update()
    {
        if (!EnergyManager.pause)
        {
            time += Time.deltaTime;
            if (nextTime <= time && !end)
            {
                SpawnObstacle();
            }
        }
    }

    private void SpawnObstacle()
    {
        nextTime = time + delay;
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Type type = prefab.GetComponent<Obstacle>().GetType();
        Obstacle obstacle = GetObjectTypeOf(type); 
        if (obstacle)
        {
            GetObjectTypeOf(type).Initialize(speed);
        }
        else
        {
            NewObstacle(prefab);
        }
    }

    private void NewObstacle(GameObject prefab)
    {
        prefab = Instantiate(prefab);
        Obstacle obstacle = prefab.GetComponent<Obstacle>();
        obstacle.Initialize(speed);
        spawnedObstacles.Add(obstacle);
    }

    private Obstacle GetObjectTypeOf(Type type)
    {
        foreach(Obstacle obstacle in spawnedObstacles)
        {
            if (obstacle.GetType() == type && obstacle.transform.position.x <= -11)
                return obstacle;
        }
        return null;
    }

    public void Stop()
    {
        end = true;
    }

    public void Continue()
    {
        end = false;
    }

    public void SpeedUp()
    {
        if (speed < maxSpeed)
            speed += increaseSpeed;
        else
            speed = maxSpeed;

        if (delay > minDelay)
            delay -= decreaseDelay;
        else
            delay = minDelay;
    }

    public float GetSpeed()
    {
        return speed;
    }
}