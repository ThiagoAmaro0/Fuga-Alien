using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearObstacle : Obstacle
{

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.x > -11 && !end)
            Move();
    }
    public override void Move()
    {
        transform.position -= Vector3.right * speed;
    }
}
