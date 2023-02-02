using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BugTest : MonoBehaviour
{
    public Transform target;
    Vector3 direction;
    Vector3 dirVec;
    public float speed;
    public float cycle = 10f;
    public float height = 1f;

    void Awake()
    {
        direction = transform.position - target.position;
        dirVec = direction.normalized;
    }

    void Update()
    {
        // X로 Cos이동
        float xWave = -Mathf.Abs(Mathf.Cos(Time.time * cycle) * height);
        // Y로 Sin이동
        float yWave = -Mathf.Abs(Mathf.Sin(Time.time * cycle) * height);

        Vector3 movePos = new Vector3(transform.position.x + dirVec.x * xWave * speed * Time.deltaTime,
                                      transform.position.y + dirVec.y * yWave * speed * Time.deltaTime);

        transform.position = movePos;
    }
}
