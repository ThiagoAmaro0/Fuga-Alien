using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Obstacle : MonoBehaviour
{
    protected float speed;
    protected bool end;
    public virtual void Initialize(float speed)
    {
        this.speed = speed;
        transform.position = new Vector3(11, Random.Range(-4f, 4f), 0);
    }
    public abstract void Move();

    private void OnEnable()
    {
        EnergyManager.PauseAction += Stop;
        EnergyManager.ContinueAction += Continue;
    }

    private void OnDisable()
    {
        EnergyManager.PauseAction -= Stop;
        EnergyManager.ContinueAction -= Continue;
    }
    public void Stop()
    {
        end = true;
    }
    public void Continue()
    {
        end = false;
    }
}
