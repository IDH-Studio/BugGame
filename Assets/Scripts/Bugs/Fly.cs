using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Bug
{
    protected override void Move()
    {
        float xWave = -(Mathf.Abs(Mathf.Cos(Time.time) * height));
        float yWave = -(Mathf.Abs(Mathf.Sin(Time.time) * height));

        Vector3 movePos = new Vector3(transform.position.x + dirVec.x * speed * Time.deltaTime * xWave,
                                      transform.position.y + dirVec.y * speed * Time.deltaTime * yWave);

        transform.position = movePos;
    }
}
