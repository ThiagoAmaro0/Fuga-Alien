using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : Obstacle
{
    private float time;
    [SerializeField] private float waveSize;
    [SerializeField] private float waveHeight;

    public override void Initialize(float _speed)
    {
        time = Random.Range(0f, 20f);
        base.Initialize(_speed);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.x > -11 && !end)
            Move();
    }
    public override void Move()
    {
        time += Time.deltaTime * waveSize + speed/10;
        float y = Mathf.Cos(time)* waveHeight;
        transform.position -= Vector3.right *speed;
        transform.position = new Vector3(transform.position.x, y, 0);
    }
}
